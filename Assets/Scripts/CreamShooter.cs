using System.Collections;
using UnityEngine;

public class CreamShooter : MonoBehaviour
{
    private const float ForceMultiplier = 0.1f;
    [SerializeField] private GameObject creamPrefab;
    [SerializeField] private Transform firePoint;
    [SerializeField] private float maxLaunchForce = 5f;
    [SerializeField] private float launchCount = 10f;
    [SerializeField] private float launchDelay = 0.1f;

    public Vector2 direction;
    public float launchForce = 10f;

    private Vector2 _startPosition;

    private WaitForSeconds _wait;

    private void Start()
    {
        _wait = new WaitForSeconds(launchDelay);

        direction = firePoint.up;

        InputSystem.Instance.OnMouseButtonDown += OnMouseButtonDown;
        InputSystem.Instance.OnMouseButtonHold += OnMouseButtonHold;
        InputSystem.Instance.OnMouseButtonUp += OnMouseButtonUp;
    }

    private void OnDestroy()
    {
        InputSystem.Instance.OnMouseButtonDown -= OnMouseButtonDown;
        InputSystem.Instance.OnMouseButtonHold -= OnMouseButtonHold;
        InputSystem.Instance.OnMouseButtonUp -= OnMouseButtonUp;
    }

    private void OnMouseButtonDown(Vector2 mousePosition)
    {
        _startPosition = mousePosition;
    }

    private void OnMouseButtonHold(Vector2 mousePosition)
    {
        var offset = _startPosition - mousePosition;

        direction = offset.normalized;
        transform.up = direction;

        var rawForce = offset.magnitude * ForceMultiplier;
        launchForce = Mathf.Clamp(rawForce, 0f, maxLaunchForce);
    }

    private void OnMouseButtonUp(Vector2 mousePosition)
    {
        StartCoroutine(ShootRoutine(direction, launchForce));
    }

    private IEnumerator ShootRoutine(Vector2 dir, float power)
    {
        for (var i = 0; i < launchCount; i++)
        {
            // 발사체 생성
            var cream = Instantiate(creamPrefab, firePoint.position, Quaternion.identity);

            // 물리 힘 적용
            var rb = cream.GetComponent<Rigidbody2D>();

            rb.AddForce(dir * power, ForceMode2D.Impulse);

            yield return _wait; // 0.1초 딜레이
        }
    }
}