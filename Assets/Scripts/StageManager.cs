using Interactables;
using System;
using System.Collections.Generic;
using Throwables;
using Throws;
using UnityEngine;
using UnityEngine.Pool;

public class StageManager : MonoBehaviour
{
    [SerializeField] private List<GameObject> throwerList;
    [SerializeField] private List<GameObject> interactableList;

    public static StageManager Instance;

    public event Action OnStageStart;
    public event Action OnStageEnd;
    public event Action OnStageFailed;
    public event Action OnStageClear;
    public event Action OnNextStage;

    public Thrower currentThrower;
    public Interactable currentInteractable;

    public int currentStage = 0;
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
        StageStart();
    }

    public void StageStart()
    {
        currentThrower = Instantiate(throwerList[currentStage]).GetComponent<Thrower>();
        currentInteractable = Instantiate(interactableList[currentStage]).GetComponent<Interactable>();

        print("start");
        OnStageStart?.Invoke();
    }

    public void StageEnd()
    {
        if (maxStage > currentStage)
            currentStage++;

        OnStageEnd?.Invoke();

        Destroy(currentThrower.gameObject);
        Destroy(currentInteractable.gameObject);
    }

    public void StageFailed()
    {
        OnStageFailed?.Invoke();
        OnStageEnd?.Invoke();
    }

    public void StageClear()
    {
        print("Stage Clear");
        OnStageClear?.Invoke();
    }

    public void NextStage()
    {
        StageEnd();
        print("StageEnd");

        OnNextStage?.Invoke();
        print("OnNextStage?.Invoke");

        StageStart();
        print("StageStart");
    }
}
