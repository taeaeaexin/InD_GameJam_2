using Interactables;
using UnityEngine;

namespace Throwables
{
    public class Cream : Throwable
    {
        [SerializeField] Sprite[] sprites;

        protected override void Awake()
        {
            base.Awake();
            
            ThrowableSprite.sprite = sprites[Random.Range(0, sprites.Length)];
        }

        protected override void Interact()
        {
            Destroy(gameObject);
        }
    }
}