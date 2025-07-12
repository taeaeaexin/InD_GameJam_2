using UnityEngine;

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
        }

        protected virtual void OnMouseButtonDown(Vector2 mousePosition)
        {
            if (IsThrown) return;
            
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
        }

        protected GameObject DoSpawn()
        {
            return Instantiate(throwablePrefab, throwPoint.position, Quaternion.identity);
        }

        public float GetThrowableCount()
        {
            return throwableCount;
        }
    }
}