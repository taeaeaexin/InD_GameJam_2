using System;
using UnityEngine;

public class InputSystem : MonoBehaviour
{
    public static InputSystem Instance;

    private bool isDragging;

    private void Awake()
    {
        if (!Instance)
        {
            Instance = this;
            DontDestroyOnLoad(gameObject);
        }
        else if (Instance != this)
        {
            Destroy(gameObject);
        }
    }

    private void Update()
    {
        var mousePos = Input.mousePosition;

        switch (isDragging)
        {
            case false when Input.GetMouseButtonDown(0):
                isDragging = true;
                OnMouseButtonDown?.Invoke(mousePos);
                break;
            case true when Input.GetMouseButton(0):
                OnMouseButtonHold?.Invoke(mousePos);
                break;
            case true when Input.GetMouseButtonUp(0):
                isDragging = false;
                OnMouseButtonUp?.Invoke(mousePos);
                break;
        }
    }

    public event Action<Vector2> OnMouseButtonDown;
    public event Action<Vector2> OnMouseButtonHold;
    public event Action<Vector2> OnMouseButtonUp;
}