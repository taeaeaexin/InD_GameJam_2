using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Rigidbody2D))]
public class Strawberry : MonoBehaviour
{
    private Rigidbody2D rb;
    private bool hasLanded = false;
    private bool isThrown = false;

    public float throwForce = 5f;

    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        rb.gravityScale = 0f; // 시작 시 무중력!
        rb.velocity = Vector2.zero;
        rb.angularVelocity = 0f;
    }

    void OnMouseDown()
    {
        if (hasLanded || isThrown) return;

        Vector3 startPos = Camera.main.ScreenToWorldPoint(Input.mousePosition);
        startPos.z = 0;
        StartCoroutine(DragAndThrow(startPos));
    }

    private System.Collections.IEnumerator DragAndThrow(Vector3 startPos)
    {
        while (Input.GetMouseButton(0))
        {
            yield return null;
        }

        Vector3 endPos = Camera.main.ScreenToWorldPoint(Input.mousePosition);
        endPos.z = 0;

        Vector2 force = (startPos - endPos) * throwForce;

        // 이제 던진다! 중력 ON + 힘 주기
        rb.gravityScale = 3f;  // 원하는 중력 세기로 조정
        rb.AddForce(force, ForceMode2D.Impulse);
        rb.angularVelocity = Random.Range(-360f, 360f);
        isThrown = true;
    }

    void OnCollisionEnter2D(Collision2D collision)
    {
        Debug.Log("충돌 감지: " + collision.collider.name);
        if (hasLanded || !isThrown) return;

        if (collision.collider.CompareTag("Cake"))
        {
            float speed = rb.velocity.magnitude;
            float rotSpeed = Mathf.Abs(rb.angularVelocity);

            Debug.Log($"속도: {speed}, 회전속도: {rotSpeed}");

            if (speed < 5f && rotSpeed < 200f) // 조건 완화
            {
                Debug.Log("착지 조건 통과 → StickToCake 예정");
                hasLanded = true;

                // 살짝 튀는 느낌
                rb.velocity = Vector2.zero;
                rb.angularVelocity = 0f;
                rb.AddForce(Vector2.up * 1.5f, ForceMode2D.Impulse);

                Invoke(nameof(StickToCake), 0.15f); // 조금만 기다렸다가 고정
            }
            else
            {
                Debug.Log("착지 조건 불충분 → StickToCake 안 함");
            }
        }
    }

    void StickToCake()
    {
        Debug.Log("StickToCake 호출됨!");
        
        rb.velocity = Vector2.zero;
        rb.angularVelocity = 0f;
        rb.gravityScale = 0f;
        rb.bodyType = RigidbodyType2D.Kinematic;
        rb.constraints = RigidbodyConstraints2D.FreezeAll; // 완전 고정
        GetComponent<Collider2D>().enabled = false;

        // 눌림 & 파묻힘 연출
        transform.position = new Vector3(transform.position.x, transform.position.y - 0.05f, transform.position.z);
        transform.localScale = new Vector3(1f, 0.9f, 1f);

        Debug.Log("딸기 고정 완료 🍓");
    }
}