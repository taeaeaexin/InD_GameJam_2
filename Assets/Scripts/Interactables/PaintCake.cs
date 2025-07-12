using UnityEngine;
using Random = UnityEngine.Random;

namespace Interactables
{
    public class PaintCake : Interactable
    {
        [SerializeField] private GameObject spritePrefab;
        [SerializeField] private Transform spriteParent;
        [SerializeField] private Sprite[] paintedCakeImages;

        private Camera _cam;

        private void Start()
        {
            _cam = Camera.main;
        }

        public override void Interact(Collision2D collision)
        {
            if (collision == null) return;

            var contactPoint = collision.GetContact(0).point;
            
            SpawnCream(contactPoint);
        }

        private bool CheckRoI(Vector2 contactPoint)
        {
            var boi = Col.bounds;
            
            return !(contactPoint.x < boi.min.x) && !(contactPoint.x > boi.max.x);
        }
        private void SpawnCream(Vector2 contactPoint)
        {
            var main = Instantiate(spritePrefab,contactPoint, Quaternion.identity, spriteParent).GetComponent<SpriteRenderer>();
            main.sprite = paintedCakeImages[Random.Range(0, paintedCakeImages.Length - 1)];
            
            var rightPos = contactPoint + Vector2.right * 0.5f;
            
            if (!CheckRoI(rightPos)) return;
            
            var right = Instantiate(spritePrefab,rightPos, Quaternion.identity, spriteParent).GetComponent<SpriteRenderer>();
            right.flipX = true;
            right.sprite = paintedCakeImages[^1];

            var leftPos = contactPoint + Vector2.left * 0.5f;
            
            if (!CheckRoI(leftPos)) return;
            
            var left = Instantiate(spritePrefab,leftPos, Quaternion.identity, spriteParent).GetComponent<SpriteRenderer>();
            left.flipX = false;
            left.sprite = paintedCakeImages[^1];
        }
    }
}