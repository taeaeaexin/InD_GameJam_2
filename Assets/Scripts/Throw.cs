using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Throw : MonoBehaviour
{
    private Vector3 startPos;
    private Rigidbody2D rb;
    private bool isDragging = false;

    public float forceMultiplier = 5f;

    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
    }

    void OnMouseDown()
    {
        startPos = Camera.main.ScreenToWorldPoint(Input.mousePosition);
        startPos.z = 0;
        isDragging = true;
    }

    void OnMouseUp()
    {
        if (isDragging)
        {
            Vector3 endPos = Camera.main.ScreenToWorldPoint(Input.mousePosition);
            endPos.z = 0;
            Vector2 force = (startPos - endPos) * forceMultiplier;
            rb.AddForce(force, ForceMode2D.Impulse);
            isDragging = false;
        }
    }
}
