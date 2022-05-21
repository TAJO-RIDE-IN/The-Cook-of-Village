using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public interface IGameDataOb
{
    void AddObserver(IObserver<GameDataManager> o);
    void RemoveObserver(IObserver<GameDataManager> o);
    void NotifyObserver();
}
public interface IObserver<T>
{
    void Change(T obj);
}

public class GameDataManager : MonoBehaviour, IGameDataOb
{
    private List<IObserver<GameDataManager>> _observers = new List<IObserver<GameDataManager>>();

    #region 싱글톤
    private static GameDataManager instance = null;
    private void Awake() //씬 시작될때 인스턴스 초기화
    {
        if (null == instance)
        {
            instance = this;
            DontDestroyOnLoad(this.gameObject);
        }
        else
        {
            Destroy(this.gameObject);
        }
    }
    public static GameDataManager Instance
    {
        get
        {
            if (null == instance)
            {
                return null;
            }
            return instance;
        }
    }
    #endregion
    private void Start()
    {
        LoadObject();
    }
    void OnEnable()
    {
        // 델리게이트 체인 추가
        SceneManager.sceneLoaded += OnSceneLoaded;
    }
    void OnDisable()
    {
        // 델리게이트 체인 제거
        SceneManager.sceneLoaded -= OnSceneLoaded;
    }

    private void OnSceneLoaded(Scene scene, LoadSceneMode mode)
    {
        LoadObject();
    }
    GameObject RestaurantDisplayUI;
    GameObject VillageDisplayUI;
    GameObject DayNightClycle;
    Coroutine runningCoroutine = null;
    private void LoadObject()
    {
        _observers.Clear();
        RestaurantDisplayUI = GameObject.Find("RestaurantDisplayUI");
        VillageDisplayUI = GameObject.Find("VillageDisplayUI");
        DayNightClycle = GameObject.Find("DayNightClycle");
        if (RestaurantDisplayUI != null)
        {
            RestaurantDisplayUI.GetComponent<DisplayUI>().AddObserver(this);
        }

        if (VillageDisplayUI != null)
        {
            VillageDisplayUI.GetComponent<DisplayUI>().AddObserver(this);
        }

        if (DayNightClycle != null)
        {
            DayNightClycle.GetComponent<LightingManager>().AddObserver(this);
        }

        if(runningCoroutine != null) //한 개의 코루틴만 실행
        {
            StopCoroutine(runningCoroutine);
        }
        runningCoroutine = StartCoroutine(UpdateLight());
    }

    private IEnumerator UpdateLight()
    {
        while (RestaurantDisplayUI != null || VillageDisplayUI != null)
        {
            TimeOfDay += Time.deltaTime * orbitSpeed;
            if (TimeOfDay > 1440)
            {
                TimeOfDay = 0;
                Day++;
            }
            yield return null;
        }
    }

    [SerializeField, Range(0, 1440)] //24시간 => 1440분
    private float timeOfDay;
    public float TimeOfDay 
    { 
        get { return timeOfDay; } 
        set 
        { 
            timeOfDay = value; 
            NotifyObserver(); 
        }
    }
    [SerializeField]
    private float orbitSpeed = 1.0f;

    [SerializeField]
    private int day = 1;
    public int Day 
    {
        get { return day; } 
        set 
        {
            day = value;
            NotifyObserver();
        } 
    }

    [SerializeField]
    private int money = 5000;
    public int Money
    {
        get { return money; }
        set
        {
            money = value;
            NotifyObserver();
        }
    }

    private void OnValidate()
    {
        NotifyObserver();
    }

    public void AddObserver(IObserver<GameDataManager> o)
    {
        _observers.Add(o);
    }
    public void RemoveObserver(IObserver<GameDataManager> o)
    {
        _observers.Remove(o);
    }
    public void NotifyObserver() //observer에 값 전달
    {
        foreach (var observer in _observers)
        {
            observer.Change(this);
        }
    }
}
