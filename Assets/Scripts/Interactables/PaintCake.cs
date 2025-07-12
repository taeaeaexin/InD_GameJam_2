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
            // Sprite.sprite = paintedCakeImage.sprite;
            Sprite.color = Color.gray;

            StageManager.Instance.StageClear();
        }
    }
}