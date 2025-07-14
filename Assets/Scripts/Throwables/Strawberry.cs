using Interactables;
using UnityEngine;

namespace Throwables
{
    public class Strawberry : Throwable
    {
        private float _elapsed;
        private bool _isStop;
        private bool _isMyTurn = true;

        protected override void OnCollisionEnter2D(Collision2D collision)
        {
            if (collision.collider.CompareTag("Failed") || collision.collider.CompareTag("Cake"))
            {
                StageManager.Instance.StageFailed();
                return;
            }
            
            if (collision.collider.CompareTag("Cream"))
            {
                StopToCollision();

                _isStop = true;

                _elapsed = 0f;
                
                transform.parent = collision.transform;

                StageManager.Instance.currentThrower.currentThrowable = null;
            }
        }

        private void OnCollisionStay2D(Collision2D other)
        {
            if (!_isMyTurn) return;
            if (!_isStop) return;

            _elapsed += Time.fixedDeltaTime;

            if (!(_elapsed >= 2f)) return;

            if (other.collider.CompareTag("Cream"))
            {
                StageManager.Instance.StageClear();
                _isMyTurn = false;
            }
            else StageManager.Instance.StageFailed();

            _isStop = false;
        }
    }
}