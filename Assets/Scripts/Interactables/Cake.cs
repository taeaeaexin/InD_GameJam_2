using System.Collections;
using UnityEngine;
using Random = UnityEngine.Random;

namespace Interactables
{
    public class Cake : Interactable
    {
        [SerializeField] private GameObject spritePrefab;
        [SerializeField] private Transform spriteParent;
        [SerializeField] private Sprite[] paintedCakeImages;

        private bool isRestarted;

        protected override void Awake()
        {
            base.Awake();

            StageManager.Instance.OnStageStart += OnRestarted;
        }

        private void OnRestarted()
        {
            if (!gameObject.activeSelf) return;

            isRestarted = true;

            StageManager.Instance.currentThrower.OnThrow += StartRoutine;
        }

        public void Clear()
        {
            if (!isRestarted) return;
            if (StageManager.Instance.currentCakeStage == 0)
                foreach (Transform t in spriteParent.transform)
                    Destroy(t.gameObject);

            gameObject.SetActive(false);

            isRestarted = false;
        }

        private void StartRoutine()
        {
            if (!gameObject.activeSelf) return;

            StartCoroutine(CheckGameClear());

            StageManager.Instance.currentThrower.OnThrow -= StartRoutine;
        }

        private IEnumerator CheckGameClear()
        {
            yield return new WaitForSeconds(5f);

            if (spriteParent.transform.childCount > 7)
                StageManager.Instance.StageClear();
            else
                StageManager.Instance.StageFailed();
        }

        public override void Interact(Collision2D collision)
        {
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
            var main = Instantiate(spritePrefab, contactPoint, Quaternion.identity, spriteParent)
                .GetComponent<SpriteRenderer>();
            main.sprite = paintedCakeImages[Random.Range(0, paintedCakeImages.Length - 1)];

            var rightPos = contactPoint + Vector2.right * 0.5f;

            if (!CheckRoI(rightPos)) return;

            var right = Instantiate(spritePrefab, rightPos, Quaternion.identity, spriteParent)
                .GetComponent<SpriteRenderer>();
            right.flipX = true;
            right.sprite = paintedCakeImages[^1];

            var leftPos = contactPoint + Vector2.left * 0.5f;

            if (!CheckRoI(leftPos)) return;

            var left = Instantiate(spritePrefab, leftPos, Quaternion.identity, spriteParent)
                .GetComponent<SpriteRenderer>();
            left.flipX = false;
            left.sprite = paintedCakeImages[^1];
        }
    }
}