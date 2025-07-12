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
    [SerializeField] private GameObject failedZone;
    [SerializeField] Camera zoom;
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

        zoom.transform.localPosition = currentInteractable.transform.localPosition + Vector3.back;
        
        // failedZone.SetActive(!currentInteractable.CompareTag("Cake"));
        
        OnStageStart?.Invoke();
    }

    public void StageRestart()
    {
        Destroy(currentThrower.gameObject);
        Destroy(currentInteractable.gameObject);
        
        StageStart();
    }

    public void StageEnd()
    {
        if (maxStage > currentStage)
        {
            currentStage++;
        }
        
        OnStageEnd?.Invoke();

        Destroy(currentThrower.gameObject);
        Destroy(currentInteractable.gameObject);
    }

    public void StageFailed()
    {
        OnStageFailed?.Invoke();
        
        StageRestart();
    }

    public void StageClear()
    {
        if (SoundManager.Instance) SoundManager.Instance.Play_R_SFX("sound_throw_", 4);
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
