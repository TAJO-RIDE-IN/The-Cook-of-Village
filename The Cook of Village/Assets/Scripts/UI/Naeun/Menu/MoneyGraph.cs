using System;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.UI;

public class MoneyGraph : MonoBehaviour
{
    public GameObject[] YaxisLine;
    public Text[] YaxisText;
    public Text[] DayText;
    public Image[] DotLine;
    public Image[] Dot;
    public Transform DotContainer;
    public ToggleControl toggleControl;
    public enum DataType { Proceeds, Consumption, Total }
    [SerializeField] private DataType type = DataType.Proceeds;
    public DataType dataType
    {
        get { return type; }
        set
        {
            type = value;
            UpdateData();
        }
    }
    private List<int> WeekProceeds = new List<int>();
    private List<int> WeekConsumption = new List<int>();
    private List<int> WeekAbsTotal = new List<int>();
    private List<int> WeekDay = new List<int>();

    private GameData gameData;
    private MoneyData moneyData;
    private int yAxisSpace = 40;
    private int MoneySpace = 1000;

    public Color PlusColor;
    public Color MinusColor;
    public Color NetGainColor;
    private void OnEnable()
    {
        gameData = GameData.Instance;
        moneyData = MoneyData.Instance;
        dataType = DataType.Proceeds;
        toggleControl.ResetToggle(2);
        UpdateData();
    }
    #region data
    public void ChangeDataType(int type)
    {
        dataType = (DataType)type;
    }

    private void ResetData()
    {
        Color color = GraphColor(dataType);
        foreach (var _dot in Dot)
        {
            _dot.gameObject.SetActive(false);
            _dot.color = color;
        }
        foreach (var _line in DotLine)
        {
            _line.gameObject.SetActive(false);
            _line.color = color;
        }
        WeekProceeds.Clear();
        WeekConsumption.Clear();
        WeekAbsTotal.Clear();
        WeekDay.Clear();
    }
    public void UpdateData()
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
        int TotalProceeds = moneyData.Proceeds[day].SalesProceeds + moneyData.Proceeds[day].TipMoney + moneyData.Proceeds[day].ResellMoney;
        int TotalConsumption = moneyData.Consumption[day];
        WeekProceeds.Add(TotalProceeds);
        WeekConsumption.Add(TotalConsumption);
        WeekAbsTotal.Add(Math.Abs(TotalProceeds - TotalConsumption));
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
                space = (int)IntRound(totalMax, -3) / 2;
                break;
        }
        space = (space.Equals(0)) ? MoneySpace : space;
        ChangeYaxisText(type, space);
        ArrangeDot(type, space);
        ArrangeLine();
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
                    YaxisText[i].text = (-space * i).ToString();
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
    /// <summary>
    /// 점 배치
    /// </summary>
    /// <param name="type">데이터 타입 입력</param>
    /// <param name="space"></param>
    private void ArrangeDot(DataType type, float space)
    {
        float dotSpace = yAxisSpace / space;
        float YPosition = 0;
        for (int i = 0; i < WeekDay.Count; i++)
        {
            Dot[i].gameObject.SetActive(true);
            Vector3 dotPosition = Dot[i].transform.localPosition;
            Vector3 container = new Vector3(0, 0, 0);
            if (type.Equals(DataType.Proceeds))
            {
                container = new Vector3(0, -yAxisSpace * 2, 0);
                YPosition = WeekProceeds[i] * dotSpace;
            }
            else if (type.Equals(DataType.Consumption))
            {
                container = new Vector3(0, yAxisSpace * 2, 0);
                YPosition = WeekConsumption[i] * -dotSpace;
            }
            else if (type.Equals(DataType.Total))
            {
                container = new Vector3(0, 0, 0);
                int total = WeekProceeds[i] - WeekConsumption[i];
                dotSpace = (total > space) ? dotSpace : -dotSpace;
                YPosition = Math.Abs(total) * dotSpace;
            }
            DotContainer.localPosition = container;
            Dot[i].transform.localPosition = new Vector3(dotPosition.x, YPosition, dotPosition.z);
        }
    }
    /// <summary>
    /// 그래프 색 변경
    /// </summary>
    /// <param name="type">데이터 타입 입력</param>
    /// <returns>색 리턴</returns>
    private Color GraphColor(DataType type)
    {
        switch (type)
        {
            case DataType.Proceeds:
                return PlusColor;
            case DataType.Consumption:
                return MinusColor;
            case DataType.Total:
                return NetGainColor;
        }
        return PlusColor;
    }
    /// <summary>
    /// 점과 점 사이 라인을 그림
    /// </summary>
    private void ArrangeLine()
    {
        for (int i = 0; i < WeekDay.Count - 1; i++)
        {
            float x1 = Dot[i].transform.localPosition.x;
            float y1 = Dot[i].transform.localPosition.y;
            float x2 = Dot[i + 1].transform.localPosition.x;
            float y2 = Dot[i + 1].transform.localPosition.y;
            float LineX = (x2 + x1) / 2;
            float LineY = (y2 + y1) / 2;
            double LineGradient = Math.Atan2((y2 - y1),  (x2 - x1)) * (180 / Math.PI);
            double LineLength = Math.Sqrt(Math.Pow((x2 - x1), 2) + Math.Pow((y2 - y1), 2));
            DotLine[i].gameObject.SetActive(true);
            DotLine[i].transform.eulerAngles = new Vector3(0, 0, (float)LineGradient);
            DotLine[i].transform.localPosition = new Vector3(LineX, LineY, 0);
            DotLine[i].rectTransform.sizeDelta = new Vector2((float)LineLength, DotLine[i].rectTransform.sizeDelta.y);
        }
    }
}
