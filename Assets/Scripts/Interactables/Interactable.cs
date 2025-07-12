using UnityEngine;

namespace Interactables
{
    public class Interactable : MonoBehaviour
    {
        protected SpriteRenderer Sprite;
        protected Collider2D Col;

        private void Awake()
        {
            Sprite = GetComponent<SpriteRenderer>();
            Col = GetComponent<Collider2D>();
        }
        
        public virtual void Interact()
        {
        }
    }
}
