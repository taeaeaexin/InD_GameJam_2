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
            if(SoundManager.Instance) SoundManager.Instance.PlaySFX(Resources.Load<AudioClip>("sound/sound_drop_1"));
            if (collision.collider.CompareTag("Failed"))
            {
                Destroy(this);
                return;
            }
            if (collision.collider.CompareTag("Target"))
            {
                StopToCollision();
            }
        }
    }
}