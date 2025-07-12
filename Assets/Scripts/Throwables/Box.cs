using System.Collections;
using System.Collections.Generic;
using Throwables;
using UnityEngine;

public class Box : Throwable
{
    protected override void Interact()
    {
        StopToCollision();
    }
}