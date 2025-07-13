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

        private void FixedUpdate()
        {
            if (!IsThrown) return;
            
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
