using Interactables;
using UnityEngine;

namespace Throwables
{
    [RequireComponent(typeof(Rigidbody2D))]
    public class Throwable : MonoBehaviour
    {
        protected SpriteRenderer ThrowableSprite;
        private Rigidbody2D _rb;
        private bool _isThrown;
        private bool _isTorque;

        protected virtual void Awake()
        {
            ThrowableSprite = GetComponent<SpriteRenderer>();
            _rb = GetComponent<Rigidbody2D>();
        }

        private void OnCollisionEnter2D(Collision2D collision)
        {
            if (!collision.collider.CompareTag("Target")) return;

            var interactable = collision.collider.GetComponent<Interactable>();

            interactable.Interact();
            
            Interact();
        }

        public void Throw(Vector2 throwDirection, float throwForce, float spreadAngle = 0f)
        {
            if (_isThrown) return;

            _isThrown = true;
            
            var direction = Spread(throwDirection, spreadAngle);
            
            _rb.AddForce(direction * throwForce, ForceMode2D.Impulse);
        }

        private Vector2 Spread(Vector2 throwDirection, float spreadAngle)
        {
            if (spreadAngle == 0f) return throwDirection;
            
            var halfAngle = spreadAngle * 0.5f;
            var randomAngle = Random.Range(-halfAngle, halfAngle);
            
            return Quaternion.Euler(0, 0, randomAngle) * throwDirection;
        }

        public void Torque(float torque)
        {
            _rb.AddTorque(torque);
        }

        protected virtual void Interact()
        {
            StopToCollision();
        }

        // fix later
        public void StopToCollision()
        {
            _rb.velocity = Vector2.zero;
            _rb.angularVelocity = 0f;
            _rb.gravityScale = 0f;
            _rb.bodyType = RigidbodyType2D.Static;
        }
    }
}