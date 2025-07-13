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
			rb.isKinematic = true; // �⺻ ����: ���� ���� �� ��
		}

		if (col != null)
		{
			col.isTrigger = false; // �⺻ ����: �浹 ó����
		}
	}

	void OnCollisionEnter2D(Collision2D collision)
	{
		if (collision.collider.CompareTag("Title_Icon"))
		{
			if (rb != null)
			{
				rb.isKinematic = false; // ���� �ۿ� ���
			}

			if (col != null)
			{
				col.isTrigger = true; // Ʈ���ŷ� ��ȯ
			}
		}
	}
}
