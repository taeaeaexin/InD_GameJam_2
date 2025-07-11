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
        Debug.Log($"isThrown: {isThrown}, hasLanded: {hasLanded}"); // 상태 확인용 로그

        // 임시로 조건 막음 → 디버깅을 위해 무조건 실행되게 함
        // if (hasLanded || !isThrown) return;

        if (collision.collider.CompareTag("Cake"))
        {
            Debug.Log("무조건 StickToCake 호출합니다 (디버깅용)");
            StickToCake();
        }
    }

    void StickToCake()
    {
        Debug.Log("StickToCake 호출됨!");

        rb.velocity = Vector2.zero;
        rb.angularVelocity = 0f;
        rb.gravityScale = 0f;
        rb.bodyType = RigidbodyType2D.Static;

        Debug.Log("딸기 고정 완료 🍓");
    }
}