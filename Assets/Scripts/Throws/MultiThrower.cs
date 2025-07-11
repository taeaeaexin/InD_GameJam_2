using System.Collections;
using Throwables;
using UnityEngine;

namespace Throws
{
    public class MultiThrower : Thrower
    {
        [SerializeField] private float throwDelay = 0.1f;

        private WaitForSeconds _wait;

        protected override void Start()
        {
            base.Start();

            _wait = new WaitForSeconds(throwDelay);
        }

        protected override void OnMouseButtonHold(Vector2 mousePosition)
        {
            base.OnMouseButtonHold(mousePosition);

            transform.up = ThrowDirection;
        }

        protected override void OnMouseButtonUp(Vector2 mousePosition)
        {
            StartCoroutine(ShootRoutine());
        }

        private IEnumerator ShootRoutine()
        {
            for (var i = 0; i < throwableCount; i++)
            {
                var cream = DoSpawn();

                var throwable = cream.GetComponent<Throwable>();
                
                throwable.Throw(ThrowDirection, ThrowForce, 20f);

                yield return _wait;
            }
        }
    }
}