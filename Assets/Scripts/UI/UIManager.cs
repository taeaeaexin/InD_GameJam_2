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

        OnStageStart();
    }

    private void OnDestroy()
    {
        StageManager.Instance.OnStageStart -= OnStageStart;
        StageManager.Instance.OnStageEnd -= OnStageEnd;
        StageManager.Instance.OnStageFailed -= OnStageFailed;
        StageManager.Instance.OnStageClear -= OnStagaClear;
        StageManager.Instance.OnNextStage -= OnNextStage;
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
}