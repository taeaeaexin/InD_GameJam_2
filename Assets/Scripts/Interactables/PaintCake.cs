using UnityEngine;
using UnityEngine.Serialization;
using UnityEngine.UI;

namespace Interactables
{
    public class PaintCake : Interactable
    {
        [SerializeField] private Image paintedCakeImage;

        override public void Interact()
        {
            // Sprite.sprite = paintedCakeImage.sprite;
            Sprite.color = Color.black;
        }
    }
}
