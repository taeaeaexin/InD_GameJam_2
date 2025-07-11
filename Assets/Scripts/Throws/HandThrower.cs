using System.Collections;
using System.Collections.Generic;
using Throwables;
using Throws;
using UnityEngine;

public class HandThrower : Thrower
{
    private Rigidbody2D rb;
    private Throwable throwable;

    protected override void Start()
    {
        base.Start();

        var throwPrefab = DoSpawn();
        rb = throwPrefab.GetComponent<Rigidbody2D>();
        throwable = throwPrefab.GetComponent<Throwable>();
        rb.isKinematic = true;
    }

    protected override void OnMouseButtonUp(Vector2 mousePosition)
    {
        rb.isKinematic = false;
        throwable.Throw(ThrowDirection, ThrowForce);
    }
}
