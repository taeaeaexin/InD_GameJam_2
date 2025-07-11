using UnityEngine;

public class LineDrawer : MonoBehaviour
{
    [SerializeField] private float lineWidth = 0.1f;
    private Camera _cam;
    private LineRenderer _line;

    private void Awake()
    {
        _line = GetComponent<LineRenderer>();
        _line.positionCount = 2;
        _line.enabled = false;
        if (_cam == null) _cam = Camera.main;
        
    }

    private void Start()
    {
        _line.startWidth = lineWidth;
        _line.endWidth = lineWidth;
        
        InputSystem.Instance.OnMouseButtonDown += OnMouseButtonDown;
        InputSystem.Instance.OnMouseButtonHold += OnMouseButtonHold;
        InputSystem.Instance.OnMouseButtonUp += OnMouseButtonUp;
    }

    private void OnMouseButtonDown(Vector2 mousePos)
    {
        var p = _cam.ScreenToWorldPoint(Input.mousePosition);
        p.z = 0f;
        _line.SetPosition(0, p);
        _line.SetPosition(1, p);
        _line.enabled = true;
    }

    private void OnMouseButtonHold(Vector2 mousePos)
    {
        var p = _cam.ScreenToWorldPoint(Input.mousePosition);
        p.z = 0f;
        _line.SetPosition(1, p);
    }

    private void OnMouseButtonUp(Vector2 obj)
    {
        _line.enabled = false;
    }
}