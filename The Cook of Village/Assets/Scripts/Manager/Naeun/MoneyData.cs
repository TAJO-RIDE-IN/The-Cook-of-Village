using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
[System.Serializable]
public class Proceeds
{
    public int SalesProceeds;
    public int TipMoney;
}

[System.Serializable]
public class MoneyInfos
{
    public int Money; //�����ϰ� �ִ� ��
    public int BankMoney; //���࿡ ���� ��
    public float BankInterest; //���� ����
    public int TotalProceeds; //�����
    public int TotalConsumption; //�Һ�
    public List<Proceeds> Proceeds = new List<Proceeds>();
    public List<int> Consumption = new List<int>();
}
public class MoneyData : DataManager
{
    #region �̱���
    private static MoneyData instance = null;
    private void Awake() //�� ���۵ɶ� �ν��Ͻ� �ʱ�ȭ
    {
        if (null == instance)
        {
            instance = this;
            //LoadDataTime();
        }
        else
        {
            Destroy(this.gameObject);
        }
    }
    public static MoneyData Instance
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
    [SerializeField]public MoneyInfos moneyInfos;
    private bool BankData = false;

    public int Money
    {
        get { return moneyInfos.Money; }
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
            moneyInfos.Money = value;
        }
    }

    public void ChangeMoneyData(int value)
    {
        if (moneyInfos.Money < value)
        {
            int money = value - moneyInfos.Money;
            moneyInfos.TotalProceeds += money;
            moneyInfos.Proceeds[GameData.Instance.Day - 1].SalesProceeds += (value - TipMoney);
        }
        else
        {
            int money = moneyInfos.Money - value;
            moneyInfos.TotalConsumption += money;
            moneyInfos.Consumption[GameData.Instance.Day - 1] += money;
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
        BankMoney += _money;
        BankData = true;
        Money -= _money;
    }
    public void ChangeBank()
    {
        if (GameData.Instance.Day % 3 == 0) //3�ϸ��� ���ں���
        {
            float _interest = UnityEngine.Random.Range(0.08f, 0.20f);
            BankInterest = (float)Math.Round(_interest, 3);
        }
        BankMoney = (int)(BankMoney * (1 + BankInterest));
    }

    public void AddMoneyList()
    {
        moneyInfos.Proceeds.Add(new Proceeds());
        moneyInfos.Consumption.Add(0);
    }
    public int TipMoney;
    public int TipCount;

    public override void SaveDataTime()
    {
        SaveData<MoneyInfos>(ref moneyInfos, "MoneyData");
    }
}
