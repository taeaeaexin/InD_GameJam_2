using System;
using Throwables;
using UnityEngine;
using UnityEngine.Serialization;

namespace Throws
{
    public class Thrower : MonoBehaviour
    {
        [SerializeField] protected GameObject throwablePrefab;
        [SerializeField] protected Transform throwPoint;
        [SerializeField] protected float throwableCount = 10f;
        [SerializeField] private float maxThrowForce = 15f;

        protected Vector2 ThrowDirection;
        protected float ThrowForce;
        protected bool IsThrown = false;
        
        private Vector2 _startPosition;

        public GameObject currentThrowable;
        
        public event Action OnThrow;
        
        protected virtual void Start()
        {
            InputSystem.Instance.OnMouseButtonDown += OnMouseButtonDown;
            InputSystem.Instance.OnMouseButtonHold += OnMouseButtonHold;
            InputSystem.Instance.OnMouseButtonUp += OnMouseButtonUp;
        }

        protected virtual void OnDestroy()
        {
            InputSystem.Instance.OnMouseButtonDown -= OnMouseButtonDown;
            InputSystem.Instance.OnMouseButtonHold -= OnMouseButtonHold;
            InputSystem.Instance.OnMouseButtonUp -= OnMouseButtonUp;

            if(currentThrowable) Destroy(currentThrowable);
        }

        protected virtual void OnMouseButtonDown(Vector2 mousePosition)
        {
            if (IsThrown) return;
            if (SoundManager.Instance) SoundManager.Instance.PlaySFXWithStop("sound_pluck");
            _startPosition = mousePosition;
        }

        protected virtual void OnMouseButtonHold(Vector2 mousePosition)
        {
            if (IsThrown) return;
            
            var offset = _startPosition - mousePosition;

            ThrowDirection = offset.normalized;
            
            ThrowForce = Mathf.InverseLerp(0, 1000, offset.magnitude) * maxThrowForce;
        }

        protected virtual void OnMouseButtonUp(Vector2 mousePosition)
        {
            IsThrown = true;
            if (SoundManager.Instance) SoundManager.Instance.StopSFX("sound_pluck");
            if (SoundManager.Instance) SoundManager.Instance.Play_R_SFX("sound_throw_", 2);
            OnThrow?.Invoke();
        }

        protected void Spawn()
        {
            currentThrowable = Instantiate(throwablePrefab, throwPoint.position, Quaternion.identity);
        }
    }
}