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
        moneyText.SellText.text = infos.Proceeds[day - 1].SalesProceeds.ToString();
        moneyText.TipText.text = infos.Proceeds[day - 1].TipMoney.ToString();
        moneyText.ConsumptionText.text = infos.Consumption[day - 1].ToString();
        moneyText.TotalMoneyText.text = (total > 0) ? "+ " + total : "- " + total;
        moneyText.TotalMoneyText.color = (total > 0) ? PlusColor : MinusColor;
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
