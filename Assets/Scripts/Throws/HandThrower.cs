using System.Collections;
using System.Collections.Generic;
using Throwables;
using Throws;
using UnityEngine;

namespace Throws
{
    public class HandThrower : Thrower
    {
        private Rigidbody2D throwableRb;
        private Throwable _throwable;

        protected override void Start()
        {
            base.Start();

            Spawn();
            throwableRb = CurrentThrowable.GetComponent<Rigidbody2D>();
            _throwable = CurrentThrowable.GetComponent<Throwable>();
            throwableRb.isKinematic = true;
        }

        protected override void OnMouseButtonUp(Vector2 mousePosition)
        {
            throwableRb.isKinematic = false;
            _throwable.Throw(ThrowDirection, ThrowForce);
        }
    }
}
