using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TruckMover : MonoBehaviour
{
    public float speed = 1f;
    private bool isMoving = false;

    void Update()
    {
        if (isMoving)
        {
            transform.Translate(Vector3.right * speed * Time.deltaTime);
        }
    }

    public void StartTruck()
    {
        isMoving = true;
        Debug.Log("🚚 트럭 출발!");
    }
}