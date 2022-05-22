using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public interface IGameDataOb
{
    void AddObserver(IObserver<GameData> o);
    void RemoveObserver(IObserver<GameData> o);
    void NotifyObserver();
}
public interface IObserver<T>
{
    void Change(T obj);
}

[System.Serializable]
public class GameInfos
{
    public int Day;
    public int Money;
    public GameInfos(int day, int money)
    {
        Day = day;
        Money = money;
    }
}

public class GameData : DataManager, IGameDataOb
{
    private List<IObserver<GameData>> _observers = new List<IObserver<GameData>>();
    private GameObject RestaurantDisplayUI;
    private GameObject VillageDisplayUI;
    private GameObject DayNightClycle;
    private Coroutine runningCoroutine = null;

    #region 싱글톤
    private static GameData instance = null;
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
    public static GameData Instance
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
    #region OnSceneLoaded
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
        runningCoroutine = StartCoroutine(UpdateTime());
    }
    #endregion
    private IEnumerator UpdateTime()
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

    [SerializeField]
    private GameInfos gameInfos;

    #region 변수
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
    public int Day
    {
        get { return gameInfos.Day; }
        set
        {
            gameInfos.Day = value;
            NotifyObserver();
            SaveDataTime();
        }
    }
    public int Money
    {
        get { return gameInfos.Money; }
        set
        {
            gameInfos.Money = value;
            NotifyObserver();
        }
    }
    #endregion
    public override void SaveDataTime()
    {
        SaveData<GameInfos>(ref gameInfos, "GameData");
        IngredientsData.Instance.SaveDataTime();
        FoodData.Instance.SaveDataTime();
    }
    #region observer

    public void AddObserver(IObserver<GameData> o)
    {
        _observers.Add(o);
    }
    public void RemoveObserver(IObserver<GameData> o)
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
    #endregion
}
