using System;
using System.Collections;
using Interactables;
using UnityEngine;
using Random = UnityEngine.Random;

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
        protected Rigidbody2D Rb;
        protected bool IsThrown;
        private bool _isTorque;
        
        protected virtual void Awake()
        {
            ThrowableSprite = GetComponent<SpriteRenderer>();
            Rb = GetComponent<Rigidbody2D>();
            Col = GetComponent<Collider2D>();
            if (SoundManager.Instance) SoundManager.Instance.Play_R_SFX("sound_throw_", 2);
        }

        protected virtual void OnCollisionEnter2D(Collision2D collision)
        {
            if (!collision.collider.CompareTag("Failed")) return;
            
            StageManager.Instance.StageFailed();
            
            if (!collision.collider.CompareTag("Target")) return;

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
            if (IsThrown) return;

            IsThrown = true;

            var direction = Spread(throwDirection, spreadAngle);
            
            Rb.AddForce(direction * throwForce, ForceMode2D.Impulse);
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
            Rb.AddTorque(torque);
        }

        protected virtual void Interact()
        {
            StopToCollision();
        }

        // fix later
        public void StopToCollision()
        {
            Rb.velocity = Vector2.zero;
            Rb.angularVelocity = 0f;
            Rb.gravityScale = 0f;
            Col.enabled = false;
        }

        public void Bound(Vector2 normal)
        {
            var incomingVel = Rb.velocity;

            var reflectedVel = Vector2.Reflect(incomingVel, normal);

            Rb.velocity = reflectedVel * restitution;
        }
    }
}