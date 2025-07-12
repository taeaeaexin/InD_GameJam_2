using UnityEngine;
using UnityEngine.UI;

namespace Interactables
{
    public class PaintCake : Interactable
    {
        [SerializeField] private Image paintedCakeImage;

        public override void Interact()
        {
            // Sprite.sprite = paintedCakeImage.sprite;
            Sprite.color = Color.gray;

            StageManager.Instance.StageClear();
        }
    }
}