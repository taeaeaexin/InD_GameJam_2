using System;
using UnityEngine;

namespace Throwables
{
    public class Box : Throwable
    {
        private float _elapsed;

        protected override void Interact()
        {
            
        }

        protected override void OnTriggerEnter2D(Collider2D collision)
        {
            if (!collision.CompareTag("DelayFail")) return;
            
            _elapsed = 0;
        }

        private void OnTriggerStay2D(Collider2D other)
        {
            if (!other.CompareTag("DelayFail")) return;
         
            _elapsed += Time.fixedDeltaTime;
            
            if (!(_elapsed >= 2f)) return;
            
            StageManager.Instance.StageFailed();
        }
    }
}