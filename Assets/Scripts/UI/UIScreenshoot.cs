using UnityEngine;
using UnityEngine.UI;

namespace UI
{
    public class UIScreenshoot : MonoBehaviour
    {
        public ZoomView zoomView;
        public RawImage image;
        private void OnEnable()
        {
            Texture2D texture2D = zoomView.LoadRandomPNG();
            image.texture = texture2D;
        }
    }
}
