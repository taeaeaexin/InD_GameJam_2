using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using System;

public class UIManager : MonoBehaviour
{
    public TextMeshProUGUI currentStageText;
    public GameObject clearUI;
    public GameObject AllClearUI;

    [Header("Guide UI")]
    public GameObject GuideUI;
    private GameObject guideUIInstance;
    
    [SerializeField] private float idleThreshold = 5f;
    private float idleTimer = 0f;
    private bool inputDetected = false;
    public static UIManager Instance { get; private set; }

    void Awake()
    {
        if (Instance == null) Instance = this;
        else Destroy(gameObject);

        DontDestroyOnLoad(gameObject);
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
        guideUIInstance = Instantiate(GuideUI);
    }


    private void Update()
    {
        inputDetected = false;

        idleTimer += Time.deltaTime;

        if (idleTimer >= idleThreshold)
        {
            guideUIInstance?.SetActive(true);
        }
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
        currentStageText.text = $"STAGE : {(StageManager.Instance.currentStage + 1).ToString()}";
    }

    public void OnStageEnd()
    {
        if (StageManager.Instance.MaxStage <= StageManager.Instance.currentStage)
            AllClearUI.SetActive(true);
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

    public void ShowClearUI()
    {
        clearUI.SetActive(true);
    }

    public void HideClearUI()
    {
        clearUI.SetActive(false);
    }

    public void OnMouseButtonDown(Vector2 mousePos)
    {
        inputDetected = true;
        idleTimer = 0f;
        guideUIInstance?.SetActive(false);
    }

    private void OnMouseButtonHold(Vector2 vector)
    {
        inputDetected = true;
        idleTimer = 0f;
        guideUIInstance?.SetActive(false);
    }

    private void OnMouseButtonUp(Vector2 vector)
    {
        inputDetected = true;
        idleTimer = 0f;
        guideUIInstance?.SetActive(false);
    }
}