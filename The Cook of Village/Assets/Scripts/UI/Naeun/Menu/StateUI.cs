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

public class StateUI : UIController
{
    public Toggle[] FameToggle;
    public Text[] GuestStateText;
    [SerializeField] public MoneyText moneyText;
    public Color PlusColor;
    public Color MinusColor;
    public Color ZeroColor;

    private GameData gameData;
    private MoneyData moneyData;
    private int day;
    public void StateUIState()
    {
        this.gameObject.SetActive(!this.gameObject.activeSelf);
        UpdateData();
    }

    private void UpdateData()
    {
        gameData = GameData.Instance;
        moneyData = MoneyData.Instance;
        day = gameData.Day;
        FameToggleState();
        GuestState();
        MoneyState();
    }

    private void MoneyState()
    {
        MoneyInfos infos = moneyData.moneyInfos;
        int total = infos.Proceeds[day - 1].SalesProceeds + infos.Proceeds[day - 1].TipMoney - infos.Consumption[day - 1];
        MoneyText(infos.Proceeds[day - 1].SalesProceeds, moneyText.SellText);
        MoneyText(infos.Proceeds[day - 1].TipMoney, moneyText.TipText);
        MoneyText(-infos.Consumption[day - 1], moneyText.ConsumptionText);
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
}
