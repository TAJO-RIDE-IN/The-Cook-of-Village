using System;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.UI;

public class MoneyGraph : MonoBehaviour
{
    public GameObject[] ProceedDot;
    public GameObject[] ConsumptionDot;
    public GameObject[] TotalDot;
    public GameObject[] YaxisLine;
    public Text[] YaxisText;
    public Text[] DayText;
    public ToggleControl toggleControl;
    public enum DataType { Proceeds, Consumption, Total }
    [SerializeField] private DataType type = DataType.Proceeds;
    public DataType dataType
    {
        get { return type; }
        set
        {
            type = value;
            AddData();
        }
    }
    private List<int> WeekProceeds = new List<int>();
    private List<int> WeekConsumption = new List<int>();
    public List<int> WeekAbsTotal = new List<int>();
    private List<int> WeekDay = new List<int>();

    private GameData gameData;
    private MoneyData moneyData;

    private int WeekTotalProceed;
    private int WeekTotalWeekConsumption;

    private int yAxisSpace = 40;
    private int MoneySpace = 1000;
    private void OnEnable()
    {
        gameData = GameData.Instance;
        moneyData = MoneyData.Instance;
        toggleControl.ResetToggle(2);
        AddData();
    }
    #region data
    public void ChangeDataType(int type)
    {
        dataType = (DataType)type;
    }

    private void ResetData()
    {
        foreach (var _dot in ProceedDot)
        {
            _dot.SetActive(false);
        }
        foreach (var _dot in ConsumptionDot)
        {
            _dot.SetActive(false);
        }
        foreach (var _dot in TotalDot)
        {
            _dot.SetActive(false);
        }
        WeekProceeds.Clear();
        WeekConsumption.Clear();
        WeekAbsTotal.Clear();
        WeekDay.Clear();
        WeekTotalProceed = 0;
        WeekTotalWeekConsumption = 0;
    }
    private void AddData()
    {
        ResetData();
        int day = gameData.Day;
        WeekDay = (day > 7) ? Enumerable.Range(day - 6, 7).ToList() : Enumerable.Range(1, day).ToList();
        foreach (var _day in WeekDay.Select((value, index) => (value, index)))
        {
            WeekText(_day.value, _day.index);
            WeekMoneyData(_day.value - 1);
        }
        ChangeYaxisSpace(dataType);
    }

    private void WeekText(int day, int index)
    {
        int[] MonthDate = GameData.Instance.MonthDateCalculation(day);
        string text = string.Format("{0:00} / ", MonthDate[1]) + string.Format("{0:00}", MonthDate[0]);
        DayText[index].text = text.ToString();
    }

    private void WeekMoneyData(int day)
    {
        int TotalProceeds = moneyData.moneyInfos.Proceeds[day].SalesProceeds + moneyData.moneyInfos.Proceeds[day].TipMoney;
        int TotalConsumption = moneyData.moneyInfos.Consumption[day];
        WeekProceeds.Add(TotalProceeds);
        WeekConsumption.Add(TotalConsumption);
        WeekAbsTotal.Add(Math.Abs(TotalProceeds - TotalConsumption));
        WeekTotalProceed += TotalProceeds;
        WeekTotalWeekConsumption -= TotalConsumption;
    }
    #endregion

    private void ChangeYaxisSpace(DataType type)
    {
        int space = 0;
        int max = WeekProceeds.Max();
        int min = WeekConsumption.Max();
        int totalMax = WeekAbsTotal.Max();
        switch (type)
        {
            case DataType.Proceeds:
                space = (int)IntRound(max, -3) / 4;
                break;
            case DataType.Consumption:
                space = (int)IntRound(min, -3) / 4;
                break;
            case DataType.Total:
                totalMax = (int)IntRound(totalMax, -3);
                space = (totalMax) / 2;
                break;
        }
        space = (space == 0) ? MoneySpace : space;
        ChangeYaxisText(type, space);
        ArrangeDot(type, space);
    }
    public double IntRound(double Value, int Digit)
    {
        double Temp = Math.Pow(10.0, Digit);
        return Math.Ceiling(Value * Temp) / Temp;
    }
    private void ChangeYaxisText(DataType type, int space)
    {
        switch (type)
        {
            case DataType.Proceeds:
                for (int i = 0; i < YaxisText.Length; i++)
                {
                    YaxisText[YaxisText.Length - 1 - i].text = (space * i).ToString();
                }
                break;
            case DataType.Consumption:
                for (int i = 0; i < YaxisText.Length; i++)
                {
                    YaxisText[i].text = (space * i).ToString();
                }
                break;
            case DataType.Total:
                for (int i = 0; i < YaxisText.Length; i++)
                {
                    YaxisText[i].text = (space * (2 - i)).ToString();
                }
                break;
        }
    }
    private void ArrangeDot(DataType type, float space)
    {
        float dotSpace = yAxisSpace / space;
        for (int i = 0; i < WeekDay.Count; i++)
        {
            if (type == DataType.Proceeds)
            {
                ProceedDot[i].SetActive(true);
                float YPosition = WeekProceeds[i] * dotSpace;
                Vector3 dot = ProceedDot[i].transform.localPosition;
                ProceedDot[i].transform.localPosition = new Vector3(dot.x, YPosition, dot.z);
            }
            else if (type == DataType.Consumption)
            {
                ConsumptionDot[i].SetActive(true);
                float YPosition = WeekConsumption[i] * -dotSpace;
                Vector3 dot = ConsumptionDot[i].transform.localPosition;
                ConsumptionDot[i].transform.localPosition = new Vector3(dot.x, YPosition, dot.z);
            }
            else if (type == DataType.Total)
            {
                TotalDot[i].SetActive(true);
                int total = WeekProceeds[i] - WeekConsumption[i];
                dotSpace = (total > space) ? dotSpace : -dotSpace;
                float YPosition = Math.Abs(total) * dotSpace;
                Vector3 dot = TotalDot[i].transform.localPosition;
                TotalDot[i].transform.localPosition = new Vector3(dot.x, YPosition, dot.z);
            }
        }
    }
}
