using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public interface IGameDataOb
{
    void AddObserver(IObserver<GameData> o);
    void AddDayObserver(IObserver<GameData> o);
    void AddGuestObserver(IObserver<GameData> o);
    void RemoveObserver(IObserver<GameData> o);
    void NotifyObserver(List<IObserver<GameData>> observer);
}
public interface IObserver<T>
{
    void Change(T obj);
}
[System.Serializable]
public class GuestCountInfos
{
    public int TotalGuest;
    public int SucceedGuest;
    public int FailGuest;
}
[System.Serializable]
public class PlayerInfos
{
    public string PlayerID;
    public string PlayerName;
    public string RestaurantName;
    public int Day;
    public int Money;
}
[System.Serializable]
public class GameInfos
{
    public PlayerInfos playerInfos;
    [Range(0, 1440)] public float TimeOfDay;
    public int Fame; //명성
    public int RainbowDrinking;
    public List<GuestCountInfos> CountInfos;
}

public class GameData : DataManager<GameData>, IGameDataOb
{
    private List<IObserver<GameData>> Observers = new List<IObserver<GameData>>();
    private List<IObserver<GameData>> DayObservers = new List<IObserver<GameData>>();
    private List<IObserver<GameData>> GuestObservers = new List<IObserver<GameData>>();
    private Coroutine runningCoroutine = null;
    public GameInfos gameInfos;
    [SerializeField] private GameManager gameManaer;
    [SerializeField] private ItemData itemData;
    [SerializeField] private FoodData foodData;
    [SerializeField] private NPCData npcData;
    [SerializeField] private MoneyData moneyData;
    [SerializeField] private InstallData installData;
    [HideInInspector] public bool isExtension;
    public void LoadObject()
    {
        Observers.Clear();
        moneyData.TipCount = 0;
        if (runningCoroutine != null) { StopCoroutine(runningCoroutine); }//한 개의 코루틴만 실행
        if (GuestCount > 0)
        {
            ChangeFame(-3 * GuestCount);
            GuestCountInfos[Day - 1].FailGuest = +GuestCount;
        }
    }
    public void PlaySceneInit()
    {
        runningCoroutine = StartCoroutine(UpdateTime());
    }
    private IEnumerator UpdateTime()
    {
        bool time = true;
        while (time)
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

    #region 변수
    public string RestaurantName
    {
        get { return gameInfos.playerInfos.RestaurantName; }
        set
        {
            gameInfos.playerInfos.RestaurantName = value;
            NotifyObserver(Observers);
        }
    }
    public string PlayerID
    {
        get { return gameInfos.playerInfos.PlayerID; }
        set
        {
            gameInfos.playerInfos.PlayerID = value;
        }
    }
    public string PlayerName
    {
        get { return gameInfos.playerInfos.PlayerName; }
        set
        {
            gameInfos.playerInfos.PlayerName = value;
        }
    }
    public float TimeOfDay //24시간 => 1440분
    { 
        get { return gameInfos.TimeOfDay; } 
        set 
        {
            gameInfos.TimeOfDay = value;
            if(CanSaveData())
            {
                UseSave = false;
                SaveDataTime("Save");
            }
            NotifyObserver(Observers); 
        }
    }

    private bool UseSave = true;
    /// <summary>
    /// 데이터를 저장할 수 있는지 확인
    /// </summary>
    /// <returns>분이 0이고 저장을 안 한 상태일 때 true 리턴</returns>
    private bool CanSaveData()
    {
        int hour6 = (int)TimeOfDay % 360;
        if(hour6 != 0)
        {
            UseSave = true;
        }
        return ((hour6 == 0) && UseSave);
    }
    public float orbitSpeed;
    public int Day
    {
        get { return gameInfos.playerInfos.Day; }
        set
        {
            gameInfos.playerInfos.Day = value;
            Potion.Instance.ResetPotion();
            moneyData.ChangeBank(npcData.LikeabilityEffect(NPCInfos.Work.Bank));
            npcData.ResetData();
            itemData.ResetData();
            ChangeMonthDate();
            AddDataList();
            NotifyObserver(DayObservers);
        }
    }
    private void AddDataList()
    {
        moneyData.AddMoneyList();
        gameInfos.CountInfos.Add(new GuestCountInfos());
}
    public int Today = 1;
    public int Date = 1;
    public int Month = 0;
    private void ChangeMonthDate()
    {
        Today = (int)Day % 7;
        int[] MonthData = MonthDateCalculation(Day);
        Date = MonthData[0];
        Month = MonthData[1];
    }

    public int[] MonthDateCalculation(int day) // 0 -> 일, 1 -> 월
    {
        int[] MonthDate = new int[2];
        int date = day;
        int month;
        for(month = 0; date > 14; month++)
        {
            date -= 14;
        }
        month = (month % 4) + 1; //1~4월
        MonthDate[0] = date;
        MonthDate[1] = month;
        return MonthDate;
    }

    public void SetTimeMorning()
    {
        TimeOfDay = 480;
        Day++;
    }

    public int Fame
    {
        get { return gameInfos.Fame; }
        set
        {
            if (value <= 500)
            {
                gameInfos.Fame = value;
            }
        }
    }
    public void ChangeFame(int value)
    {
        Fame += value;
    }
    public int RainbowDrinking
    {
        get { return gameInfos.RainbowDrinking; }
        set
        {
            gameInfos.RainbowDrinking = value;
            if (gameInfos.RainbowDrinking % 5 == 0)
            {
                isExtension = true;
            }
        }
    }
    #region Guest
    [SerializeField] private int guestCount;
    public int GuestCount
    {
        get { return guestCount; }
        set
        {
            if (guestCount < value && value != 0)
            {
                GuestCountInfos[Day - 1].TotalGuest++;
            }
            guestCount = value;
            NotifyObserver(GuestObservers);
        }
    }
    public void GuestCountData(int count)
    {
        if(count > 0)
        {
            GuestCountInfos[Day - 1].SucceedGuest++;
        }
        else
        {
            GuestCountInfos[Day - 1].FailGuest++;
        }
    }
    public List<GuestCountInfos> GuestCountInfos
    {
        get { return gameInfos.CountInfos; }
        set
        {
            gameInfos.CountInfos = value;
            NotifyObserver(GuestObservers);
        }
    }
    #endregion
    #endregion
    public void ResetData() //시작 화면으로 돌아왔을때 데이터 리셋
    {
        LoadDataTime("Default");
    }
    public override void SaveDataTime(string PlayName)
    {
        if(Instance == this && gameManaer.gameMode == GameManager.GameMode.Default && !PlayerID.Equals("0"))
        {
            if (!PlayName.Equals("Default"))
            {
                PlayName = PlayerName + "_" + PlayerID;
            }
            SaveData<GameInfos>(ref gameInfos, "GameData", PlayName);
            itemData.SaveDataTime(PlayName);
            foodData.SaveDataTime(PlayName);
            npcData.SaveDataTime(PlayName);
            moneyData.SaveDataTime(PlayName);
            installData.SaveDataTime(PlayName);
        }
    }
    public void LoadDataTime(string PlayName)
    {
        if (!PlayName.Equals("Default"))
        {
            PlayName = PlayerName + "_" + PlayerID;
        }
        if (!FileExists(PlayName))
        {
            SaveDataTime(PlayName);
        }
        else
        {
            LoadData<GameInfos>(ref gameInfos, "GameData", PlayName);
            LoadData<MoneyInfos>(ref moneyData.moneyInfos, "MoneyData", PlayName);
            LoadArrayData<ItemType>(ref itemData.ItemType, "ItemData", PlayName);
            LoadArrayData<FoodTool>(ref foodData.foodTool, "FoodData", PlayName);
            LoadArrayData<NPCInfos>(ref npcData.npcInfos, "NPCData", PlayName);
            installData.LoadData(PlayName);
        }
    }
    #region observer
    public void AddObserver(IObserver<GameData> o)
    {
        Observers.Add(o);
    }
    public void AddDayObserver(IObserver<GameData> o)
    {
        DayObservers.Add(o);
    }
    public void AddGuestObserver(IObserver<GameData> o)
    {
        GuestObservers.Add(o);
    }
    public void RemoveObserver(IObserver<GameData> o)
    {
        Observers.Remove(o);
    }
    public void NotifyObserver(List <IObserver<GameData>> observer) //observer에 값 전달
    {
        foreach(var _observer in observer)
        {
            _observer.Change(this);
        }
    } 
    #endregion
}
