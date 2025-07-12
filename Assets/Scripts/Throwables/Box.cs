using System.Collections;
using System.Collections.Generic;
using Throwables;
using UnityEngine;

public class Box : Throwable
{
    private float stopTimer = 0f;
    private float stopThreshold = 0.15f;
    private float requiredStopTime = 0.8f;

    private void TriggerTruck()
    {
        TruckMover truck = FindObjectOfType<TruckMover>();
        if (truck != null && !truck.hasStarted)
        {
            Debug.Log("🚚 Box 멈춤 감지 → 트럭 출발!");
            truck.StartTruck();
        }
    }

    private void StartTruckDriving()
    {
        Debug.Log("✅ Box 멈춘지 1초 경과, 트럭 운전 시작!");

        TruckMover truck = FindObjectOfType<TruckMover>();
        if (truck != null && !truck.hasStarted)
        {
            Debug.Log("🚚 Box 멈춤 감지 → 트럭 출발!");
            truck.StartTruck();
        }
    }

    protected override void Interact()
    {
        StopToCollision();
    }
}