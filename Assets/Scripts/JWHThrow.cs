using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class JWHThrow : MonoBehaviour
{
    [SerializeField] private float ThrowPower;
    [SerializeField] private Rigidbody2D rb;
    [SerializeField] private Candle candle;
    private bool isDragging = false;
    private Vector3 offset;
    private Vector2 throwVector;

    void Start()
    {
        rb.isKinematic = true;
    }

    private void OnMouseDown()
    {
        if (candle.IsThrow) return;

        isDragging = true;
        rb.isKinematic = true;
        offset = transform.position - Camera.main.ScreenToWorldPoint(Input.mousePosition).normalized;
    }

    private void OnMouseDrag()
    {
        if (candle.IsThrow) return;

        Vector3 mouseWorld = Camera.main.ScreenToWorldPoint(Input.mousePosition).normalized;
        mouseWorld.z = 0;
        Vector3 newPos = mouseWorld + offset;

        ThrowPower = (transform.position - newPos).magnitude * 100;
        throwVector = (transform.position - newPos).normalized * ThrowPower;
    }

    private void OnMouseUp()
    {
        if (candle.IsThrow) return;
        candle.ThrowCandle();

        isDragging = false;
        rb.isKinematic = false;
        rb.AddForce(throwVector, ForceMode2D.Impulse);
        rb.AddTorque(ThrowPower);
    }
}
