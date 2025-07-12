using UnityEngine;
using System.Collections;
using Throws;

[RequireComponent(typeof(Rigidbody2D))]
public class Cake : Thrower
{
    private Rigidbody2D rb;
    private bool hasLanded = false;
    private bool isThrown = false;

    public float throwForce = 10f;
    public float stopThreshold = 0.2f;
    public float requiredStopTime = 1.0f;
    private float stopTimer = 0f;

protected override void Start()
    {
        Debug.Log("🔄 Cake.Start() called");
        rb = GetComponent<Rigidbody2D>();
        rb.bodyType = RigidbodyType2D.Static;
    }

    protected override void OnMouseButtonUp(Vector2 mousePosition)
    {
        if (isThrown) return;

        rb.bodyType = RigidbodyType2D.Dynamic;
        rb.gravityScale = 1f;
        rb.velocity = Vector2.zero;
        rb.angularVelocity = 0f;
        rb.AddForce(ThrowDirection * ThrowForce, ForceMode2D.Impulse);

        isThrown = true;
    }

    void Update()
    {
        Debug.Log("🟢 Cake.Update() running");

        if (isThrown && rb.bodyType == RigidbodyType2D.Dynamic)
        {
            Debug.Log($"[Cake Debug] vel={rb.velocity.magnitude:F3}, ang={Mathf.Abs(rb.angularVelocity):F3}, stopTimer={stopTimer:F2}");

            if (rb.velocity.magnitude < stopThreshold && Mathf.Abs(rb.angularVelocity) < 1f)
            {
                stopTimer += Time.deltaTime;
                if (stopTimer >= requiredStopTime)
                {
                    TriggerTruck();
                }
            }
            else
            {
                stopTimer = 0f;
            }
        }
    }

    void OnBecameInvisible()
    {
        if (!isThrown) return;

        TruckMover truck = FindObjectOfType<TruckMover>();
        if (truck != null)
        {
            if (!truck.hasStarted)
            {
                Debug.Log("❌ 케이크를 트럭 출발 전에 밖으로 날려서 실패!");
                Destroy(gameObject);
            }
            else
            {
                Debug.Log("✅ 케이크가 트럭 출발 후에 안전하게 화면 밖으로 사라졌습니다.");
                Destroy(gameObject);
            }
        }
    }

    private void TriggerTruck()
    {
        Debug.Log("🟨 TriggerTruck() 진입");

        TruckMover truck = FindObjectOfType<TruckMover>();
        if (truck == null)
        {
            Debug.LogWarning("❗ TruckMover가 씬에서 발견되지 않았습니다.");
            return;
        }

        if (!truck.hasStarted)
        {
            Debug.Log("✅ TriggerTruck() 조건 통과: 트럭 출발 시작.");
            truck.StartTruck();
            this.enabled = false;
        }
        else
        {
            Debug.Log("⚠️ TriggerTruck() 조건 실패: 이미 트럭이 출발함.");
        }
    }
    protected override void OnDestroy()
    {
        if (InputSystem.Instance != null)
        {
            InputSystem.Instance.OnMouseButtonDown -= OnMouseButtonDown;
            InputSystem.Instance.OnMouseButtonHold -= OnMouseButtonHold;
            InputSystem.Instance.OnMouseButtonUp -= OnMouseButtonUp;
        }
    }
}