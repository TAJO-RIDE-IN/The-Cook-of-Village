using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public interface IGameDataOb
{
    void AddObserver(IObserver<GameData> o);
    void RemoveObserver(IObserver<GameData> o);
    void DayNotifyObserver();
    void NotifyObserver();
}
public interface IObserver<T>
{
    void Change(T obj);
}

[System.Serializable]
public class GameInfos
{
    public string RestaurantName;
    public int Day;
    public int Month;
    public int Money;
    public int BankMoney;
    public float BankInterest;
    public int Turnover;
    public int RainbowDrinking;
}

public class GameData : DataManager, IGameDataOb
{
    private List<IObserver<GameData>> _observers = new List<IObserver<GameData>>();
    private List<IObserver<GameData>> DayObservers = new List<IObserver<GameData>>();
    private GameObject UIDisplay;
    private Coroutine runningCoroutine = null;

    #region 싱글톤
    private static GameData instance = null;
    private void Awake() //씬 시작될때 인스턴스 초기화
    {
        if (null == instance)
        {
            instance = this;
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
    public void LoadObject()
    {
        _observers.Clear();
        TipCount = 0;
        UIDisplay = GameObject.Find("DisplayUI");
        if (runningCoroutine != null) { StopCoroutine(runningCoroutine); }//한 개의 코루틴만 실행
        runningCoroutine = StartCoroutine(UpdateTime());
    }
    private IEnumerator UpdateTime()
    {
        while (UIDisplay != null)
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
    public string RestaurantName
    {
        get { return gameInfos.RestaurantName; }
        set
        {
            gameInfos.RestaurantName = value;
            NotifyObserver();
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
    public int Day
    {
        get { return gameInfos.Day; }
        set
        {
            gameInfos.Day = value;
            Today = (int)value % 7;
            gameInfos.Month = value / 14 % 4 + 1;
            ShopCount.ResetShopCount();
            Potion.Instance.ResetPotion();
            ChangeBank();
            NotifyObserver();
            SaveDataTime();
        }
    }
    public int Today = 1;
    public void SetTimeMorning()
    {
        timeOfDay = 480;
        Day++;
    }

    public int Money
    {
        get { return gameInfos.Money; }
        set
        {
            if(gameInfos.Money < value)
            {
                gameInfos.Turnover += value - gameInfos.Money;
            }
            gameInfos.Money = value;
            NotifyObserver();
        }
    }
    public int BankMoney
    {
        get { return gameInfos.BankMoney; }
        set
        {
            gameInfos.BankMoney = value;
        }
    }
    public float BankInterest
    {
        get { return gameInfos.BankInterest; }
        set
        {
            gameInfos.BankInterest = value;
        }
    }
    private void ChangeBank()
    {
        if(Day % 3 == 0) //3일마다 이자변경
        {
            float _interest = UnityEngine.Random.Range(0.08f, 0.20f);
            BankInterest = (float)Math.Round(_interest, 3);
        }
        BankMoney = (int)(BankMoney* (1 + BankInterest));
    }
    public int TipMoney;
    public int TipCount;

    public int RainbowDrinking
    {
        get { return gameInfos.RainbowDrinking; }
        set
        {
            gameInfos.RainbowDrinking = value;
        }
    }
    #endregion
    public override void SaveDataTime()
    {
        SaveData<GameInfos>(ref gameInfos, "GameData");
        ItemData.Instance.SaveDataTime();
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
        foreach(var observer in _observers)
        {
            observer.Change(this);
        }
    } 
    public void DayNotifyObserver() //observer에 값 전달
    {
        foreach(var observer in _observers)
        {
            observer.Change(this);
        }
    }
    #endregion
}
