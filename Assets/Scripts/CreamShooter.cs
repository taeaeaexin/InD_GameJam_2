using System.Collections;
using Throws;
using UnityEngine;

public class CreamShooter : Thrower
{
    [SerializeField] private float launchDelay = 0.1f;

    private Vector2 _startPosition;

    private WaitForSeconds _wait;

    protected override void Start()
    {
        base.Start();
        
        _wait = new WaitForSeconds(launchDelay);
    }

    protected override void OnMouseButtonHold(Vector2 mousePosition)
    {
        base.OnMouseButtonHold(mousePosition);
        
        transform.up = ThrowDirection;
    }

    protected override void OnMouseButtonUp(Vector2 mousePosition)
    {
        StartCoroutine(ShootRoutine(ThrowDirection, ThrowForce));
    }

    private IEnumerator ShootRoutine(Vector2 dir, float power)
    {
        for (var i = 0; i < launchCount; i++)
        {
            var cream = Instantiate(spawnPrefab, throwPoint.position, Quaternion.identity);

            var rb = cream.GetComponent<Rigidbody2D>();

            rb.AddForce(dir * power, ForceMode2D.Impulse);

            yield return _wait;
        }
    }
}