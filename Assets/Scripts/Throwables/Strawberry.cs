using UnityEngine;

namespace Throwables
{
    [RequireComponent(typeof(Rigidbody2D))]
    public class Strawberry : Throwable
    {
        private bool isStuck = false;
        private Vector3 originalScale;
        private Vector3 offsetFromCream;

        void Start()
        {
            originalScale = transform.localScale;
        }

        private void OnCollisionEnter2D(Collision2D collision)
        {
            if (isStuck) return;

            if (collision.collider.CompareTag("Cream"))
            {
                isStuck = true;

                Rigidbody2D rb = GetComponent<Rigidbody2D>();
                rb.velocity = Vector2.zero;
                rb.bodyType = RigidbodyType2D.Kinematic; // Use Kinematic to follow parent smoothly
                rb.freezeRotation = true;

                // Get contact point and attach at exact hit location
                Vector2 contactPoint = collision.contacts[0].point;
                Vector3 stickPos = new Vector3(contactPoint.x, contactPoint.y, transform.position.z);
                transform.position = stickPos;

                // Instead of parenting, follow the cream with a fixed offset
                offsetFromCream = transform.position - collision.collider.transform.position;
                StartCoroutine(FollowCream(collision.collider.transform));

                GetComponent<Collider2D>().enabled = false;

                Debug.Log("🍓");
            }
        }

        private System.Collections.IEnumerator FollowCream(Transform cream)
        {
            while (true)
            {
                transform.position = cream.position + offsetFromCream;
                yield return null;
            }
        }
    }
}