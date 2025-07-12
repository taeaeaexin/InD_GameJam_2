using UnityEngine;

namespace Interactables
{
    public class MakeBurger : Interactable
    {
        public float targetTime = 5f;

        private float _elapsed;

        private bool _hasEntered;

        private bool _isClear;

        private void FixedUpdate()
        {
            if (_hasEntered && !_isClear)
            {
                _elapsed += Time.fixedDeltaTime;

                if (!(_elapsed >= targetTime)) return;
                StageManager.Instance.StageClear();
                _isClear = true;
            }
        }

        private void OnTriggerEnter2D(Collider2D other)
        {
            if (other.CompareTag("Failed")) StageManager.Instance.StageRestart();
            if (!other.CompareTag("Throwable")) return;

            _elapsed = 0f;
            _hasEntered = true;
        }

        private void OnTriggerExit2D(Collider2D other)
        {
            if (!_hasEntered) return;

            _elapsed = 0f;
            _hasEntered = false;
        }
    }
}