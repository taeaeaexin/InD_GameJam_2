using UnityEngine;
using System.Collections;

[RequireComponent(typeof(Rigidbody2D))]
public class Cake : MonoBehaviour
{
    private Rigidbody2D rb;
    private bool hasLanded = false;
    private bool isThrown = false;

    public float throwForce = 10f;
    public float stopThreshold = 0.05f;
    public float requiredStopTime = 1.0f;
    private float stopTimer = 0f;

    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        rb.bodyType = RigidbodyType2D.Static;
    }

    void OnMouseDown()
    {
        if (hasLanded || isThrown) return;

        Vector3 startPos = Camera.main.ScreenToWorldPoint(Input.mousePosition);
        startPos.z = 0;
        StartCoroutine(DragAndThrow(startPos));
    }

    private IEnumerator DragAndThrow(Vector3 startPos)
    {
        while (Input.GetMouseButton(0))
        {
            yield return null;
        }

        Vector3 endPos = Camera.main.ScreenToWorldPoint(Input.mousePosition);
        endPos.z = 0;

        Vector2 force = (startPos - endPos) * throwForce;

        rb.bodyType = RigidbodyType2D.Dynamic;
        rb.gravityScale = 1f;
        rb.velocity = Vector2.zero;
        rb.angularVelocity = 0f;
        rb.AddForce(force, ForceMode2D.Impulse);
        isThrown = true;
    }

    void Update()
    {
        if (isThrown && rb.bodyType == RigidbodyType2D.Dynamic)
        {
            if (rb.velocity.magnitude < stopThreshold && Mathf.Abs(rb.angularVelocity) < 1f)
            {
                stopTimer += Time.deltaTime;
                if (stopTimer >= requiredStopTime)
                {
                    Debug.Log("🍰 케이크 멈춤 → 트럭 출발!");

                    TruckMover truck = FindObjectOfType<TruckMover>();
                    if (truck != null)
                    {
                        truck.StartTruck();
                        this.enabled = false;
                    }
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
        Debug.Log("🍰 케이크가 화면 밖으로 나감!");
        Destroy(gameObject);
    }
}