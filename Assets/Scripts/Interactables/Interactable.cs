using Throwables;
using UnityEngine;

namespace Interactables
{
    public class Interactable : MonoBehaviour
    {
        protected SpriteRenderer Sprite;
        protected Collider2D Col;
        protected Rigidbody2D Rb;

        protected virtual void Awake()
        {
            Sprite = GetComponent<SpriteRenderer>();
            Col = GetComponent<Collider2D>();
            Rb = GetComponent<Rigidbody2D>();
        }
        
        public virtual void Interact(Collision2D collision)
        {
            
        }
    }
}
