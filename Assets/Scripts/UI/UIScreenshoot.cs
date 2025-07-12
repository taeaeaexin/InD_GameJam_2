using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

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
