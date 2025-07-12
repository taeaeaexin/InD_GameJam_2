using UnityEngine;

namespace Throwables
{
	public class Takoyaki : Throwable
	{
		protected override void OnCollisionEnter2D(Collision2D collision)
		{
			// »ç¿îµå Àç»ý
			if (SoundManager.Instance)
				SoundManager.Instance.PlaySFX(Resources.Load<AudioClip>("sound/sound_drop_1"));

			if (collision.collider.CompareTag("Target"))
			{
				Rigidbody2D rb = GetComponent<Rigidbody2D>();
				if (rb != null)
				{

					// ¹°¸® ¸ØÃã
					rb.velocity = Vector2.zero;
					rb.angularVelocity = 0f;
					rb.isKinematic = true;
				}
			}
		}
	}
}
