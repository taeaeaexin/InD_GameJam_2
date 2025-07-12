using Throwables;
using UnityEngine;

namespace Throws
{
    public class HandThrower : Thrower
    {
        private Rigidbody2D _rb;
        private Throwable _throwable;

        protected override void Start()
        {
            base.Start();

            var throwPrefab = DoSpawn();
            _rb = throwPrefab.GetComponent<Rigidbody2D>();
            _throwable = throwPrefab.GetComponent<Throwable>();
            _rb.isKinematic = true;
        }

        protected override void OnMouseButtonUp(Vector2 mousePosition)
        {
            _rb.isKinematic = false;
            _throwable.Throw(ThrowDirection, ThrowForce);
        }
    }
}
