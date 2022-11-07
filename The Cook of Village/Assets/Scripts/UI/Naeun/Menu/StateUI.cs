using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

[System.Serializable]
public class MoneyText
{
    public Text SellText;
    public Text TipText;
    public Text ConsumptionText;
    public Text TotalMoneyText;
}

public class StateUI : UIController, IObserver<MoneyData>, IObserver<GameData>
{
    public Toggle[] FameToggle;
    public Text[] GuestStateText;
    [SerializeField] public MoneyText moneyText;
    public MoneyGraph moneyGraph;
    public Color PlusColor;
    public Color MinusColor;
    public Color ZeroColor;

    private GameData gameData;
    private MoneyData moneyData;
    private int day;
    public void StateUIState()
    {
        this.gameObject.SetActive(!this.gameObject.activeSelf);
        gameData = GameData.Instance;
        moneyData = MoneyData.Instance;
        AddObserver(moneyData, gameData);
        UpdateData();
    }

    private void UpdateData()
    {
        day = gameData.Day;
        moneyGraph.UpdateData();
        FameToggleState();
        GuestState();
        MoneyState();
    }

    private void MoneyState()
    {
        int salesProceeds = moneyData.Proceeds[day - 1].SalesProceeds;
        int TipProceeds = moneyData.Proceeds[day - 1].TipMoney;
        int Consumption = moneyData.Proceeds[day - 1].TipMoney;
        int total = salesProceeds + TipProceeds - Consumption;
        MoneyText(salesProceeds, moneyText.SellText);
        MoneyText(TipProceeds, moneyText.TipText);
        MoneyText(-Consumption, moneyText.ConsumptionText);
        MoneyText(total, moneyText.TotalMoneyText);
    }
    private void MoneyText(int _money, Text _text)
    {
        string moneyText = _money.ToString();
        if(_money > 0)
        {
            moneyText = "+" + _money;
            _text.color = PlusColor;
        }
        else if(_money < 0)
        {
            _text.color = MinusColor;
        }
        else
        {
            _text.color = ZeroColor;
        }
        _text.text = moneyText;
    }
    private void GuestState()
    {
        GuestCountInfos infos = gameData.GuestCountInfos[day - 1];
        GuestStateText[0].text = infos.TotalGuest.ToString();
        GuestStateText[1].text = infos.SucceedGuest.ToString();
        GuestStateText[2].text = infos.FailGuest.ToString();
    }

    private void FameToggleState()
    {
        foreach(var _toggle in FameToggle)
        {
            _toggle.isOn = false;
        }
        int fame = (int)(gameData.Fame / 100);
        FameToggle[fame].isOn = true;
    }

    public void AddObserver(IMoneyDataOb money, IGameDataOb game)
    {
        money.AddObserver(this);
        game.AddDayObserver(this);
        game.AddGuestObserver(this);
    }
    public void Change(MoneyData obj)
    {
        if (obj is MoneyData)
        {
            var MoneyData = obj;
            UpdateData();
        }
    }
    public void Change(GameData obj)
    {
        if (obj is GameData)
        {
            var GameData = obj;
            UpdateData();
        }
    }
}
