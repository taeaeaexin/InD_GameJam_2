using System;
using System.Collections.Generic;
using Interactables;
using Throws;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.Serialization;

public class StageManager : MonoBehaviour
{
    public static StageManager Instance;

    [SerializeField] private GameObject cakePrefabs;
    [SerializeField] private List<GameObject> cakeThrowerList;
    [SerializeField] private List<GameObject> throwerList;
    [SerializeField] private List<GameObject> interactableList;
    [SerializeField] private GameObject failedZone;
    [SerializeField] private Camera zoom;

    public Thrower currentThrower;
    public Interactable currentInteractable;
    public Interactable cake;

    private int _currentStage;
    
    public int currentCakeStage;
    public int allStage;
    private Camera _cam;

    public int MaxStage { get; private set; }

    private void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
        }
        else
        {
            Destroy(gameObject);
        }
    }

    private void Start()
    {
        _cam = Camera.main;
        MaxStage = cakeThrowerList.Count + throwerList.Count;
        
        cake = Instantiate(cakePrefabs).GetComponent<Interactable>();

        cake.gameObject.SetActive(false);

        StageStart();
    }

    public event Action OnStageStart;
    public event Action OnStageEnd;
    public event Action OnStageFailed;
    public event Action OnStageClear;
    public event Action OnNextStage;

    private bool IsCakeStage()
    {
        return allStage % 2 == 1;
    }

    public void StageStart()
    {
        SetRandomCameraBackgroundColor();
        
        if (allStage >= MaxStage)
        {
            UIManager.Instance.ShowAllClearUI();
            return;
        }
        
        if (IsCakeStage() && currentCakeStage < cakeThrowerList.Count)
        {
            cake.gameObject.SetActive(true);
            currentInteractable = cake;
            currentThrower = Instantiate(cakeThrowerList[currentCakeStage]).GetComponent<Thrower>();
        }
        else if (_currentStage < throwerList.Count)
        {
            currentInteractable = Instantiate(interactableList[_currentStage]).GetComponent<Interactable>();
            currentThrower = Instantiate(throwerList[_currentStage]).GetComponent<Thrower>();
        }

        zoom.transform.localPosition = currentInteractable.transform.localPosition + Vector3.back;

        OnStageStart?.Invoke();
    }

    public void StageRestart()
    {
        if (IsCakeStage() && currentCakeStage < cakeThrowerList.Count)
        {
            Destroy(currentThrower.gameObject);
            ((Cake)cake).Clear();
        }
        else
        {
            Destroy(currentThrower.gameObject);
            Destroy(currentInteractable.gameObject);
        }

        StageStart();
    }

    public void StageEnd()
    {
        OnStageEnd?.Invoke();

        if (IsCakeStage())
        {
            cake.gameObject.SetActive(false);

            currentCakeStage++;
        }
        else
        {
            Destroy(currentInteractable.gameObject);
            
            _currentStage++;
        }

        allStage++;
        
        Destroy(currentThrower.gameObject);
    }

    public void StageFailed()
    {
        OnStageFailed?.Invoke();

        StageRestart();
    }

    public void StageClear()
    {
        if (SoundManager.Instance) SoundManager.Instance.Play_R_SFX("sound_cheer_", 4);
        
        OnStageClear?.Invoke();
    }

    public void NextStage()
    {
        StageEnd();

        OnNextStage?.Invoke();

        StageStart();
    }
    public void SetRandomCameraBackgroundColor()
    {
        var r = UnityEngine.Random.Range(100, 151) / 255f;
        var g = UnityEngine.Random.Range(100, 151) / 255f;
        var b = UnityEngine.Random.Range(100, 151) / 255f;

        _cam.backgroundColor = new Color(r, g, b);
    }

}