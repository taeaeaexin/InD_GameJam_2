using UnityEngine;

public class Title_Spelling : MonoBehaviour
{
	private Rigidbody2D rb;
	private Collider2D col;

	void Start()
	{
		rb = GetComponent<Rigidbody2D>();
		col = GetComponent<Collider2D>();

		if (rb != null)
		{
			rb.isKinematic = true; // 기본 상태: 물리 적용 안 함
		}

		if (col != null)
		{
			col.isTrigger = false; // 기본 상태: 충돌 처리함
		}
	}

	void OnCollisionEnter2D(Collision2D collision)
	{
		if (collision.collider.CompareTag("Title_Icon"))
		{
			if (rb != null)
			{
				rb.isKinematic = false; // 물리 작용 허용
			}

			if (col != null)
			{
				col.isTrigger = true; // 트리거로 전환
			}
		}
	}
}
