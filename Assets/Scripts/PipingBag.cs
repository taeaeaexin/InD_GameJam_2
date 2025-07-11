using System.Collections;
using Throws;
using UnityEngine;

public class PipingBag : Thrower
{
    [SerializeField] private float launchDelay = 0.1f;

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
        for (var i = 0; i < throwableCount; i++)
        {
            var cream = DoSpawn();

            var rb = cream.GetComponent<Rigidbody2D>();

            DoThrow(rb);

            yield return _wait;
        }
    }
}