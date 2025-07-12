using Interactables;
using UnityEngine;

namespace Throwables
{
    [RequireComponent(typeof(Rigidbody2D))]
    public class Throwable : MonoBehaviour
    {
        [Header("탄성 계수")]
        [Tooltip("1 = 완전 탄성 반사(속도 유지), 0.5 = 에너지 50% 손실")]
        [Range(0f, 1f)]
        [SerializeField] protected float restitution = 1f;
        
        protected SpriteRenderer ThrowableSprite;
        protected Collider2D Col;
        private Rigidbody2D _rb;
        private bool _isThrown;
        private bool _isTorque;

        protected virtual void Awake()
        {
            ThrowableSprite = GetComponent<SpriteRenderer>();
            _rb = GetComponent<Rigidbody2D>();
            Col = GetComponent<Collider2D>();
        }

        protected virtual void OnCollisionEnter2D(Collision2D collision)
        {
            if (!collision.collider.CompareTag("Target"))
            {
                StageManager.Instance.StageFailed();
                return;
            }

            var interactable = collision.collider.GetComponent<Interactable>();

            interactable.Interact(collision);
            
            Interact();
            
            Bound(collision.contacts[0].normal);
        }

        protected virtual void OnTriggerEnter2D(Collider2D collision)
        {
            if (!collision.CompareTag("Target")) return;

            var interactable = collision.GetComponent<Interactable>();

            interactable.Interact(null);
            
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
            Col.enabled = false;
        }

        public void Bound(Vector2 normal)
        {
            var incomingVel = _rb.velocity;

            var reflectedVel = Vector2.Reflect(incomingVel, normal);

            _rb.velocity = reflectedVel * restitution;
        }
    }
}