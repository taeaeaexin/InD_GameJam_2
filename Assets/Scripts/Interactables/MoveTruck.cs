using System.Collections;
using UnityEngine;
using Random = UnityEngine.Random;

namespace Interactables
{
    public class MoveTruck : Interactable
    {
        [Header("흔들림 세기")] [Tooltip("최대 이동 거리 (유닛)")]
        public float magnitude = 0.05f;

        [Tooltip("흔들림 속도 (클립 주기)")] public float roughness = 25f;

        public float targetTime = 5f;

        private float _elapsed;

        private bool _hasEntered = false;

        private Vector3 _initialPosition;
        private Vector3 _originalPosition;
        private float _seed;
        private Coroutine _shakeRoutine;

        protected override void Awake()
        {
            base.Awake();

            _initialPosition = transform.position;
            _seed = Random.Range(-1000f, 1000f);
        }

        private void Start()
        {
            StartShake();
        }

        private void OnDestroy()
        {
            StopShake();
        }

        private void OnTriggerEnter2D(Collider2D other)
        {
            if (!other.CompareTag("Throwable")) return;

            _elapsed = 0f; // 타이머 초기화
            _hasEntered = true; // 동작 실행 플래그 리셋
        }

        private void OnTriggerExit2D(Collider2D other)
        {
            if (!_hasEntered) return;
            
            _elapsed = 0f;
            _hasEntered = false;
        }

        private void OnTriggerStay2D(Collider2D other)
        {
            if (!_hasEntered) return;
            if (!other.CompareTag("Throwable")) return;
            if (other.GetComponent<Rigidbody2D>().velocity.magnitude > 2f)
            {
                _elapsed = 0f;
                return;
            }
            
            _elapsed += Time.fixedDeltaTime;

            if (!(_elapsed >= targetTime)) return;

            _hasEntered = false;
            StageManager.Instance.StageClear();
        }

        private void StartShake()
        {
            _shakeRoutine ??= StartCoroutine(ShakeLoop());
        }

        private void StopShake()
        {
            if (_shakeRoutine != null) StopCoroutine(_shakeRoutine);
            _shakeRoutine = null;
            transform.localPosition = _initialPosition;
        }

        private IEnumerator ShakeLoop()
        {
            while (true)
            {
                var x = (Mathf.PerlinNoise(_seed, Time.time * roughness) - 0.5f) * 2f;
                var y = (Mathf.PerlinNoise(Time.time * roughness, _seed) - 0.5f) * 2f;
                transform.localPosition = _initialPosition + new Vector3(x, y, 0f) * magnitude;
                yield return new WaitForFixedUpdate();
            }
        }
    }
}