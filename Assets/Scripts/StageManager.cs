using Interactables;
using System;
using System.Collections.Generic;
using Throwables;
using Throws;
using UnityEngine;

public class StageManager : MonoBehaviour
{
    [SerializeField] private List<Thrower> throwerList;
    [SerializeField] private List<Interactable> interactableList;

    public static StageManager Instance;

    public event Action OnStageStart;
    public event Action OnStageEnd;
    public event Action OnStageFailed;
    public event Action OnStageSucceed;

    private Thrower currentThrower;
    private Interactable currentInteractable;

    public int currentStage;
    public int maxStage = 3;

    private void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
            DontDestroyOnLoad(gameObject);
        }
        else
        {
            Destroy(gameObject);
        }
    }
    void Start()
    {
        currentStage = 0;

        StageStart();
    }

    public void StageStart()
    {
        var rand = UnityEngine.Random.Range(0, throwerList.Count);

        currentThrower = Instantiate(throwerList[rand]);
        currentInteractable = Instantiate(interactableList[rand]);

        OnStageStart?.Invoke();

        currentStage++;
    }

    public void StageEnd()
    {
        OnStageEnd?.Invoke();

        Destroy(currentThrower);
        Destroy(currentInteractable);
    }

    public void StageFailed()
    {
        OnStageFailed?.Invoke();
        OnStageEnd?.Invoke();
    }

    public void StageClear()
    {
        print("Stage Clear");
        OnStageSucceed?.Invoke();
        OnStageEnd?.Invoke();
    }
}
