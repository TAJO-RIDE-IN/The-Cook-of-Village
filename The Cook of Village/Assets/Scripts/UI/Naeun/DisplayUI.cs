using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class DisplayUI : MonoBehaviour, IObserver<GameData>
{
    public Text Money;
    public Text TimeText;
    public Text AmPmText;
    public Text DayText;
    public Text TodayText;
    private bool Am;
    private Dictionary<int, string> Today =  new Dictionary<int, string>
    {
        {1, "월요일" }, {2, "화요일" }, {3, "수요일" }, {4, "목요일"}, {5, "금요일"}, {6, "토요일"}, {0, "일요일"}
    };
    private Dictionary<bool, string> AmPm = new Dictionary<bool, string>
    {
        {true, "AM" }, {false, "PM" }
    };

    private int[] Time(float currentTime)
    {
        int[] time = new int[2];
        int hour = (int)currentTime / 60;
        Am = (hour < 12);
        hour = (hour > 12) ? hour-12 : hour;
        time[0] = hour;  //hour
        time[1] = (int) currentTime % 60 / 10;
        return time;
    }

    public void ChangeDisplay(float time, int month, int date, int today)
    {
        string min = Time(time)[1].ToString();
        TimeText.text = string.Format("{0:00} : ", Time(time)[0]) + min.PadRight(2, '0');
        AmPmText.text = AmPm[Am];
        DayText.text = month.ToString() + " 월 " + date.ToString() + " 일 ";
        Money.text = MoneyData.Instance.Money.ToString();
        TodayText.text = Today[today];
    }
    private void Start()
    {
        AddObserver(GameData.Instance);
    }

    public void AddObserver(IGameDataOb o)
    {
        o.AddObserver(this);
    }

    public void Change(GameData obj)
    {
        if (obj is GameData)
        {
            var GameData = obj;
            ChangeDisplay(GameData.TimeOfDay, GameData.Month, GameData.Date, GameData.Today);
        }
    }
}
