using System.Collections;
using UnityEngine;
using Random = UnityEngine.Random;

namespace Interactables
{
    public class MakeBurger : Interactable
    {
        public float targetTime = 5f;

        private float _elapsed;

        private bool _hasEntered = false;

        private bool isClear;
        protected override void Awake()
        {
            base.Awake();
        }

        private void OnTriggerEnter2D(Collider2D other)
        {
            if (other.CompareTag("Failed")) StageManager.Instance.StageRestart();
            if (!other.CompareTag("Throwable")) return;

            _elapsed = 0f; // 타이머 초기화
            _hasEntered = true; // 동작 실행 플래그 리셋
        }

        private void OnTriggerExit2D(Collider2D other)
        {
            if (!_hasEntered) return;

            _elapsed = 0f;
            _hasEntered = false;
            Debug.Log("트리거 벗어남 → 타이머 리셋");
        }

        private void FixedUpdate()
        {
            if (_hasEntered && !isClear)
            {
                _elapsed += Time.fixedDeltaTime;
                Debug.Log($"트리거 내 머문 시간: {_elapsed:F2}s");

                if (!(_elapsed >= targetTime)) return;
                StageManager.Instance.StageClear();
                isClear = true;
            }
        }
        
    }
}