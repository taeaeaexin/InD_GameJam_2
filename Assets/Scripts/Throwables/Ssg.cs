using System;
using System.Collections;
using UnityEngine;

namespace Throwables
{
    public class Ssg : Throwable
    {
        private float _elapsed;
        private bool _isEnter;
        
        private void Start()
        {
            Camera.main.orthographicSize = 4f;
        }
        
        protected override void Interact()
        {
        }

        protected override void OnTriggerEnter2D(Collider2D other)
        {
            if (!other.CompareTag("Target")) return;
            _isEnter = true;
        }

        private void OnTriggerExit2D(Collider2D other)
        {
            if (!other.CompareTag("Target")) return;
            _isEnter = false;
        }

        private void FixedUpdate()
        {
            if (!IsThrown) return;
            if (!_isEnter) return;
            
            if (Rb.velocity.magnitude > 0.5f)
            {
                _elapsed = 0f;
                return;
            }
            
            _elapsed += Time.fixedDeltaTime;

            if (!(_elapsed >= 3f)) return;
            
            StageManager.Instance.StageClear();
            
            Camera.main.orthographicSize = 5f;
        }
    }
}
