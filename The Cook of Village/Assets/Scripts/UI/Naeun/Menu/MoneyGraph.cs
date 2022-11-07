using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.UI;

public class MoneyGraph : MonoBehaviour
{
    public GameObject[] ProceedDot;
    public GameObject[] ConsumptionDot;
    public GameObject[] YaxisLine;
    public Text[] YaxisText;
    public Text[] DayText;

    private List<int> WeekProceeds = new List<int>();
    private List<int> WeekConsumption = new List<int>();
    private List<int> WeekDay = new List<int>();

    private GameData gameData;
    private MoneyData moneyData;

    private void OnEnable()
    {
        gameData = GameData.Instance;
        moneyData = MoneyData.Instance;
        ResetData();
        AddData();
    }
    private void ResetData()
    {
        foreach(var _dot in ProceedDot)
        {
            _dot.SetActive(false);
        }
        foreach (var _dot in ConsumptionDot)
        {
            _dot.SetActive(false);
        }
        WeekProceeds.Clear();
        WeekConsumption.Clear();
        WeekDay.Clear();
    }
    private void AddData()
    {
        int day = gameData.Day;
        WeekDay = (day > 7) ? Enumerable.Range(day - 6, 7).ToList() : Enumerable.Range(1, day).ToList();
        foreach (var _day in WeekDay.Select((value, index) => (value, index)))
        {
            WeekText(_day.value);
            WeekMoneyData(_day.value - 1);
        }

    }

    private void WeekText(int day)
    {
        int[] MonthDate = GameData.Instance.MonthDateCalculation(day);
        string text = string.Format("{0:00} / ", MonthDate[1]) + string.Format("{0:00}", MonthDate[0]);
        DayText[day].text = text.ToString();
    }

    private void WeekMoneyData(int day)
    {
        int TotalProceeds = moneyData.moneyInfos.Proceeds[day].SalesProceeds + moneyData.moneyInfos.Proceeds[day].TipMoney;
        WeekProceeds.Add(TotalProceeds);
        WeekConsumption.Add(moneyData.moneyInfos.Consumption[day]);
    }

}
