using UnityEngine;

namespace Throwables
{
    public class Strawberry : Throwable
    {
        private float _elapsed;
        private bool _isStop;

        protected override void OnCollisionEnter2D(Collision2D collision)
        {
            if (collision.collider.CompareTag("Cream"))
            {
                StopToCollision();

                _isStop = true;

                _elapsed = 0f;
            }
        }

        private void OnCollisionStay2D(Collision2D other)
        {
            if (!_isStop) return;

            _elapsed += Time.fixedDeltaTime;

            if (!(_elapsed >= 2f)) return;

            if (other.collider.CompareTag("Cream")) StageManager.Instance.StageClear();
            else StageManager.Instance.StageFailed();

            _isStop = false;
        }
    }
}