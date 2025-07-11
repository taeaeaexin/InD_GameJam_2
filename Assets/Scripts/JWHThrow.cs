using System.Collections;
using System.Collections.Generic;
using Throws;
using Unity.VisualScripting;
using UnityEngine;

public class JwhThrow : Thrower
{
    [SerializeField] private float ThrowPower;
    private Rigidbody2D rb;
    [SerializeField] private Candle candle;
    private bool isDragging = false;
    private Vector3 offset;
    private Vector2 throwVector;

    protected override void Start()
    {
        base.Start();
        
        var cake = DoSpawn();
        rb = cake.GetComponent<Rigidbody2D>();
        rb.isKinematic = true;
    }

    private void OnMouseUp()
    {
        if (candle.IsThrow) return;
        candle.ThrowCandle();

        isDragging = false;
        rb.isKinematic = false;
        //DoThrow(rb);
        rb.AddTorque(ThrowPower);
    }
}
