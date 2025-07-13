using UnityEngine;

namespace Throwables
{
	public class Takoyaki : Throwable
	{
		public float _elapsed;
		public bool _isStop;

		protected override void OnTriggerEnter2D(Collider2D other)
		{
			if (other.CompareTag("Failed"))
			{
				StageManager.Instance.StageFailed();
				return;
			}

			if (other.CompareTag("Target"))
			{
				if (SoundManager.Instance)
					SoundManager.Instance.PlaySFX(Resources.Load<AudioClip>("sound/sound_drop_1"));

				_elapsed += Time.deltaTime;

				if (!(_elapsed >= 0.01f)) return;

				StopToCollision();
				((CircleCollider2D) Col).radius = 1f;
				_isStop = true;
				_elapsed = 0f;
			}
		}

		void OnTriggerStay2D(Collider2D other)
		{
			if (!_isStop) return;

			_elapsed += Time.fixedDeltaTime;

			if (!(_elapsed >= 2f)) return;

			if (other.CompareTag("Target")) StageManager.Instance.StageClear();
			else StageManager.Instance.StageFailed();

			_isStop = false;
		}
	}
}
