using UnityEngine;

public class Match_fire : MonoBehaviour
{
	public Sprite[] fireSprites;        // 불꽃 애니메이션용 스프라이트 배열
	public float changeInterval = 0.25f; // 변경 간격 (초)

	private SpriteRenderer spriteRenderer;
	private float timer;

	void Start()
	{
		spriteRenderer = GetComponent<SpriteRenderer>();
		if (fireSprites.Length > 0)
		{
			spriteRenderer.sprite = fireSprites[Random.Range(0, fireSprites.Length)];
		}
	}

	void Update()
	{
		spriteRenderer.sprite = fireSprites[Random.Range(0, fireSprites.Length)];
		timer += Time.deltaTime;
	}
}
