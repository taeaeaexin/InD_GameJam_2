using System.Collections;
using System.Collections.Generic;
using Throwables;
using Throws;
using UnityEngine;

namespace Throws
{
    public class HandThrower : Thrower
    {
        private Rigidbody2D _throwableRb;
        private Throwable _throwable;

        protected override void Start()
        {
            base.Start();

            Spawn();
            
            _throwableRb = CurrentThrowable.GetComponent<Rigidbody2D>();
            _throwable = CurrentThrowable.GetComponent<Throwable>();
            _throwableRb.isKinematic = true;
        }
        
        protected override void OnMouseButtonHold(Vector2 mousePosition)
        {
            if (IsThrown) return;
            
            base.OnMouseButtonHold(mousePosition);
        }

        protected override void OnMouseButtonUp(Vector2 mousePosition)
        {
            _throwableRb.isKinematic = false;
            _throwable.Throw(ThrowDirection, ThrowForce);
        }
    }
}
