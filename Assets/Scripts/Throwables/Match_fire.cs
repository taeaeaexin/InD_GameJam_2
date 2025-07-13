using UnityEngine;

public class Match_fire : MonoBehaviour
{
    public Sprite[] fireSprites;
    public float changeInterval = 0.25f;

    private SpriteRenderer _spriteRenderer;
    private float _timer;

    private void Start()
    {
        _spriteRenderer = GetComponent<SpriteRenderer>();
        if (fireSprites.Length > 0) _spriteRenderer.sprite = fireSprites[Random.Range(0, fireSprites.Length)];
    }

    private void Update()
    {
        _spriteRenderer.sprite = fireSprites[Random.Range(0, fireSprites.Length)];
        _timer += Time.deltaTime;
    }
}