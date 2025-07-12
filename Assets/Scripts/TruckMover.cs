using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TruckMover : MonoBehaviour
{
    public bool hasStarted = false;
    public float speed = 1f;
    private bool isMoving = false;
    private float moveTimer = 0f;
    public float moveDuration = 15f;

    public float verticalJitterAmount = 0.05f;
    public float jitterInterval = 0.1f;
    public float backwardForce = 0.5f;
    public LayerMask packageLayer;

    public GameObject[] packages;
    public Transform screenRightBound;

    private Vector3 originalPosition;

    void Start()
    {
        originalPosition = transform.position;
    }

    void Update()
    {
        if (isMoving)
        {
            transform.Translate(Vector3.right * speed * Time.deltaTime);
            moveTimer += Time.deltaTime;

            if (moveTimer >= moveDuration)
            {
                isMoving = false;
                Debug.Log("🚚 트럭 정지!");
                return;
            }

            if (transform.position.x > screenRightBound.position.x)
            {
                isMoving = false;

                // Determine result based on package state
                bool anyPackageLeftBehind = false;
                foreach (var pkg in packages)
                {
                    if (pkg != null && pkg.transform.position.y <= 0f) // adjust ground threshold if needed
                    {
                        anyPackageLeftBehind = true;
                        break;
                    }
                }

                if (anyPackageLeftBehind)
                {
                    Debug.Log("❌ 트럭은 떠났고, 택배는 땅에 있습니다. 게임 오버!");
                }
                else
                {
                    Debug.Log("✅ 트럭이 무사히 떠났고, 택배도 살아남았습니다. 성공!");
                }

                return;
            }
        }
    }

    public void StartTruck()
    {
        if (hasStarted) return;
        hasStarted = true;
        isMoving = true;
        moveTimer = 0f;
        Debug.Log("🚚 트럭 출발!");
        StartCoroutine(ShakeAndPush());
    }

    private IEnumerator ShakeAndPush()
    {
        while (isMoving)
        {
            float verticalOffset = Random.Range(-verticalJitterAmount, verticalJitterAmount);
            transform.position = new Vector3(transform.position.x, originalPosition.y + verticalOffset, transform.position.z);

            // Apply small backward force to objects on truck
            Collider2D[] hits = Physics2D.OverlapBoxAll(transform.position, new Vector2(5f, 2f), 0f, packageLayer);
            foreach (var hit in hits)
            {
                Rigidbody2D rb = hit.attachedRigidbody;
                if (rb != null && !rb.isKinematic)
                {
                    rb.AddForce(Vector2.left * backwardForce, ForceMode2D.Force);
                }
            }

            yield return new WaitForSeconds(jitterInterval);
        }

        transform.position = new Vector3(transform.position.x, originalPosition.y, transform.position.z); // Reset Y pos
    }
}