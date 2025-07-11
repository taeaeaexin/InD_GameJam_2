using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class Candle : MonoBehaviour
{
    private bool isThrow;
    public bool IsThrow => isThrow;
    private Rigidbody2D rb;

    private void Start()
    {
        rb = GetComponent<Rigidbody2D>();
    }
    public void ThrowCandle()
    {
        isThrow = true;
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if(collision.gameObject.CompareTag("Cake"))
        {
            rb.velocity = Vector2.zero;         // 속도 즉시 정지
            rb.angularVelocity = 0f;            // 회전도 멈춤
            rb.isKinematic = true;              // 물리 꺼서 안 밀리게
            rb.gravityScale = 0f;               // 중력도 제거
        }
    }
}
