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
        GameObject.Find("RestaurantDisplayUI").GetComponent<DisplayUI>().AddObserver(this);
        GameObject.Find("DayNightClycle").GetComponent<LightingManager>().AddObserver(this);
    }
    private void Update()
    {
        TimeOfDay += Time.deltaTime * orbitSpeed;
        if (TimeOfDay > 24)
        {
            TimeOfDay = 0;
            Day++;
        }
    }


    [SerializeField, Range(0, 24)]
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
