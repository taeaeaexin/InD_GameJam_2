using UnityEngine;

public class Sprite_Hide : MonoBehaviour // 시작하자 마자 숨기기
{
    SpriteRenderer spriteRenderer;
    void Start()
    {
        spriteRenderer = GetComponent<SpriteRenderer>();
        spriteRenderer.sprite = null;
    }
}


