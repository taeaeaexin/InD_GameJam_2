using UnityEngine;

namespace Throwables
{
    public class Sora_Bread : Throwable
    {
        public Sprite w_sprite;//��ũ��
        public Sprite d_sprite;//����
		private SpriteRenderer spriteRenderer;

        private void Start()
        {
			base.Awake();
			spriteRenderer = GetComponent<SpriteRenderer>();
        }
        protected override void OnCollisionEnter2D(Collision2D collision)
		{
			// ���� ���
			if (SoundManager.Instance)
				SoundManager.Instance.PlaySFX(Resources.Load<AudioClip>("sound/sound_drop_1"));

			if (collision.collider.CompareTag("Target"))
			{
				Rigidbody2D rb = GetComponent<Rigidbody2D>();
				if (rb != null)
				{
					// �浹 ��뿡�� Two_Wkf ��������
					Two_Wkf col = collision.collider.GetComponent<Two_Wkf>();

					// ���� ����
					rb.velocity = Vector2.zero;
					rb.angularVelocity = 0f;
					rb.isKinematic = true;
					// col�� �����ϰ�, ���� ������ �б� ó��
					if (col != null)
					{
						if (col.col == "D")
						{
							spriteRenderer.sprite = d_sprite;
						}
						else if (col.col == "W")
						{
							spriteRenderer.sprite = w_sprite;
						}
						col.image_ch();
					}
				}
			}
		}
	}
}