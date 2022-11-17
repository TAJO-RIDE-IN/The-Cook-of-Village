using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public interface IMoneyDataOb
{
    void AddObserver(IObserver<MoneyData> o);
    void NotifyObserver();
}

[System.Serializable]
public class Proceeds
{
    public int SalesProceeds;
    public int TipMoney;
}

[System.Serializable]
public class MoneyInfos
{
    public int Money; //소지하고 있는 돈
    public int BankMoney; //은행에 넣은 돈
    public float BankInterest; //은행 이자
    public int TotalProceeds; //매출액
    public int TotalConsumption; //소비
    public List<Proceeds> Proceeds = new List<Proceeds>();
    public List<int> Consumption = new List<int>();
}
public class MoneyData : DataManager<MoneyData>, IMoneyDataOb
{
    private List<IObserver<MoneyData>> _observers = new List<IObserver<MoneyData>>();
    public MoneyInfos moneyInfos;
    public GameData gameData;
    private bool BankData = false;
    public int TipMoney;
    public int TipCount;
    public int Money
    {
        get { return gameData.gameInfos.playerInfos.Money; }
        set
        {
            if(!BankData)
            {
                ChangeMoneyData(value);
                if (TipCount >= 5)
                {
                    value += TipMoney;
                    moneyInfos.TotalProceeds += TipMoney;
                    moneyInfos.Proceeds[GameData.Instance.Day - 1].TipMoney += TipMoney;
                }
            }
            BankData = false;
            gameData.gameInfos.playerInfos.Money = value;
            NotifyObserver();
        }
    }

    public void ChangeMoneyData(int value)
    {
        if (moneyInfos.Money < value)
        {
            int money = value - moneyInfos.Money;
            TotalProceeds += money;
           Proceeds[GameData.Instance.Day - 1].SalesProceeds += (value - TipMoney);
        }
        else
        {
            int money = moneyInfos.Money - value;
            TotalConsumption += money;
            Consumption[GameData.Instance.Day - 1] += money;
        }
    }
    public List<Proceeds> Proceeds
    {
        get { return moneyInfos.Proceeds; }
        set
        {
            moneyInfos.Proceeds = value;
        }
    }
    public List<int> Consumption
    {
        get { return moneyInfos.Consumption; }
        set
        {
            moneyInfos.Consumption = value;
        }
    }
    public int TotalProceeds
    {
        get { return moneyInfos.TotalProceeds; }
        set
        {
            moneyInfos.TotalProceeds = value;
        }
    }
    public int TotalConsumption
    {
        get { return moneyInfos.TotalConsumption; }
        set
        {
            moneyInfos.TotalConsumption = value;
        }
    }
    public int BankMoney
    {
        get { return moneyInfos.BankMoney; }
        set
        {
            moneyInfos.BankMoney = value;
        }
    }
    public float BankInterest
    {
        get { return moneyInfos.BankInterest; }
        set
        {
            moneyInfos.BankInterest = value;
        }
    }
    public void UseBankMoney(int _money)
    {
        NPCData.Instance.ChangeLikeability(NPCInfos.Work.Bank, "PlayerUse");
        BankMoney += _money;
        BankData = true;
        Money -= _money;
    }
    public void ChangeBank(float percent)
    {
        if (GameData.Instance.Day % 3 == 0) //3일마다 이자변경
        {
            float _interest = UnityEngine.Random.Range(percent, percent*2);
            BankInterest = (float)Math.Round(_interest, 3);
        }
        BankMoney = (int)(BankMoney * (1 + BankInterest));
    }

    public void AddMoneyList()
    {
        moneyInfos.Proceeds.Add(new Proceeds());
        moneyInfos.Consumption.Add(0);
}

    public override void SaveDataTime(string PlayNum)
    {
        SaveData<MoneyInfos>(ref moneyInfos, "MoneyData" + PlayNum);
    }

    public void AddObserver(IObserver<MoneyData> o)
    {
        _observers.Add(o);
    }

    public void NotifyObserver()
    {
        foreach(var observer in _observers)
        {
            observer.Change(this);
        }
    }
}
