using System.Collections;
using System.Collections.Generic;
using Throwables;
using UnityEngine;

[RequireComponent(typeof(Rigidbody2D))]
public class Strawberry : Throwable
{
    protected override void Interact()
    {
        StopToCollision();
    }

}