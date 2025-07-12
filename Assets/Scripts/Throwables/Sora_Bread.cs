using UnityEngine;

namespace Throwables
{
    public class Sora_Bread : Throwable
    {
        public Sprite w_sprite;//생크림
        public Sprite d_sprite;//초코
		private SpriteRenderer spriteRenderer;

        private void Start()
        {
			base.Awake();
			spriteRenderer = GetComponent<SpriteRenderer>();
        }
        protected override void OnCollisionEnter2D(Collision2D collision)
		{
			// 사운드 재생
			if (SoundManager.Instance)
				SoundManager.Instance.PlaySFX(Resources.Load<AudioClip>("sound/sound_drop_1"));

			if (collision.collider.CompareTag("Target"))
			{
				Rigidbody2D rb = GetComponent<Rigidbody2D>();
				if (rb != null)
				{
					// 충돌 상대에서 Two_Wkf 가져오기
					Two_Wkf col = collision.collider.GetComponent<Two_Wkf>();

					// 물리 멈춤
					rb.velocity = Vector2.zero;
					rb.angularVelocity = 0f;

					// col이 존재하고, 내부 값으로 분기 처리
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