using System.Collections;
using Throwables;
using UnityEngine;

namespace Throws
{
    public class MultiThrower : Thrower
    {
        [SerializeField] private Sprite thrownSprite;
        
        [SerializeField] private float throwDelay = 0.1f;

        private WaitForSeconds _wait;
        private SpriteRenderer _spriteRenderer;

        protected override void Start()
        {
            base.Start();
            SoundManager.Instance.Play_R_SFX("sound/sound_bottle_opener_", 2);
            _wait = new WaitForSeconds(throwDelay);
            _spriteRenderer = GetComponent<SpriteRenderer>();
        }

        protected override void OnMouseButtonHold(Vector2 mousePosition)
        {
            if (IsThrown) return;
            
            base.OnMouseButtonHold(mousePosition);

            transform.up = ThrowDirection;
        }

        protected override void OnMouseButtonUp(Vector2 mousePosition)
        {
            if (IsThrown) return;
            
            base.OnMouseButtonUp(mousePosition);
            
            StartCoroutine(ShootRoutine());
            
            _spriteRenderer.sprite = thrownSprite;
        }

        private IEnumerator ShootRoutine()
        {
            for (var i = 0; i < throwableCount; i++)
            {
                Spawn();

                var throwable = currentThrowable.GetComponent<Throwable>();
                
                throwable.Throw(ThrowDirection, ThrowForce, 40f);

                yield return _wait;
            }
        }
    }
}