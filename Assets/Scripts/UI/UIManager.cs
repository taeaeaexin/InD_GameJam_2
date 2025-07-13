using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;

public class UIManager : MonoBehaviour
{
    public TextMeshProUGUI currentStageText;
    public GameObject clearUI;
    public GameObject allClearUI;
    public GameObject statusUI;

    [Header("Guide UI")] public GameObject guideUI;

    [SerializeField] private float idleThreshold = 5f;
    private GameObject _guideUIInstance;
    private float _idleTimer;
    private bool _inputDetected;
    public static UIManager Instance { get; private set; }

    private void Awake()
    {
        if (Instance == null) Instance = this;
        else Destroy(gameObject);
    }

    private void Start()
    {
        StageManager.Instance.OnStageStart += OnStageStart;
        StageManager.Instance.OnStageEnd += OnStageEnd;
        StageManager.Instance.OnStageFailed += OnStageFailed;
        StageManager.Instance.OnStageClear += OnStagaClear;
        StageManager.Instance.OnNextStage += OnNextStage;

        InputSystem.Instance.OnMouseButtonDown += OnMouseButtonDown;
        InputSystem.Instance.OnMouseButtonHold += OnMouseButtonHold;
        InputSystem.Instance.OnMouseButtonUp += OnMouseButtonUp;

        OnStageStart();
        _guideUIInstance = Instantiate(guideUI);
    }
    
    private void Update()
    {
        _inputDetected = false;

        _idleTimer += Time.deltaTime;

        if (_idleTimer >= idleThreshold) _guideUIInstance?.SetActive(true);
    }

    private void OnDestroy()
    {
        StageManager.Instance.OnStageStart -= OnStageStart;
        StageManager.Instance.OnStageEnd -= OnStageEnd;
        StageManager.Instance.OnStageFailed -= OnStageFailed;
        StageManager.Instance.OnStageClear -= OnStagaClear;
        StageManager.Instance.OnNextStage -= OnNextStage;

        InputSystem.Instance.OnMouseButtonDown -= OnMouseButtonDown;
        InputSystem.Instance.OnMouseButtonHold -= OnMouseButtonHold;
        InputSystem.Instance.OnMouseButtonUp -= OnMouseButtonUp;
    }
    
    public void OnStageStart()
    {
        currentStageText.text = $"STAGE : {(StageManager.Instance.allStage + 1).ToString()}";
        
        allClearUI.SetActive(false);
        statusUI.SetActive(true);
    }

    public void OnStageEnd()
    {
        if (StageManager.Instance.MaxStage <= StageManager.Instance.allStage)
            allClearUI.SetActive(true);
    }

    public void OnStageFailed()
    {
    }

    public void OnStagaClear()
    {
        ShowClearUI();
    }

    private void OnNextStage()
    {
        HideClearUI();
    }

    public void GameEnd()
    {
        allClearUI.SetActive(true);
        statusUI.SetActive(false);
        SceneManager.LoadScene("Title", LoadSceneMode.Single);
    }

    public void ShowClearUI()
    {
        clearUI.SetActive(true);
    }

    public void HideClearUI()
    {
        clearUI.SetActive(false);
    }

    public void ShowAllClearUI()
    {
        allClearUI.SetActive(true);
    }
    
    public void HideAllClearUI()
    {
        allClearUI.SetActive(false);
    }

    public void OnMouseButtonDown(Vector2 mousePos)
    {
        _inputDetected = true;
        _idleTimer = 0f;
        _guideUIInstance?.SetActive(false);
    }

    private void OnMouseButtonHold(Vector2 vector)
    {
        _inputDetected = true;
        _idleTimer = 0f;
        _guideUIInstance?.SetActive(false);
    }

    private void OnMouseButtonUp(Vector2 vector)
    {
        _inputDetected = true;
        _idleTimer = 0f;
        _guideUIInstance?.SetActive(false);
    }
}