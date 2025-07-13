using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Two_Wkf : MonoBehaviour
{
    public string col;// D, W
    public SpriteRenderer spriteRenderer;
    public Sprite ch_spr;

    public void image_ch()
    {
        spriteRenderer.sprite = ch_spr;
    }
}
