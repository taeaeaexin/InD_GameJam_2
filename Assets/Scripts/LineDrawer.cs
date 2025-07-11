using UnityEngine;

public class LineDrawer : MonoBehaviour
{
    public Camera cam; // 화면→월드 변환용 카메라
    private LineRenderer line;
    private Vector2 startPos;

    private void Awake()
    {
        line = GetComponent<LineRenderer>();
        line.positionCount = 2;
        line.enabled = false;
        if (cam == null) cam = Camera.main;
    }

    private void Start()
    {
        InputSystem.Instance.OnMouseButtonDown += OnMouseButtonDown;
        InputSystem.Instance.OnMouseButtonHold += OnMouseButtonHold;
        InputSystem.Instance.OnMouseButtonUp += OnMouseButtonUp;
    }

    private void OnMouseButtonDown(Vector2 mousePos)
    {
        startPos = cam.ScreenToWorldPoint(mousePos);
        line.enabled = true;
        line.SetPosition(0, startPos);
    }

    private void OnMouseButtonHold(Vector2 mousePos)
    {
        var currPos = cam.ScreenToWorldPoint(mousePos);
        line.SetPosition(1, currPos);
    }

    private void OnMouseButtonUp(Vector2 obj)
    {
        line.enabled = false;
    }
}