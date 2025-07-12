using Interactables;
using UnityEngine;

namespace Throwables
{
    public class Raisin : Throwable
    {
        [SerializeField] Sprite[] sprites;

        protected override void Awake()
        {
            base.Awake();
            Torque(200f);
            ThrowableSprite.sprite = sprites[Random.Range(0, sprites.Length)];
        }

        protected override void OnCollisionEnter2D(Collision2D collision)
        {
             SoundManager.Instance.PlaySFX(Resources.Load<AudioClip>("sound/sound_drop_1"));
            if (collision.collider.CompareTag("Target"))
            {
                Rigidbody2D rb = GetComponent<Rigidbody2D>();
                if (rb != null)
                {
                    
                    rb.velocity = Vector2.zero;         // 이동 정지
                    rb.angularVelocity = 0f;            // 회전 정지
                }
            }
        }
    }
}