using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class DisplayUI : MonoBehaviour, IObserver<GameData>
{
    public Text Money;
    public Text TimeText;
    public Text DayText;
    public Text TodayText;
    private Dictionary<int, string> Today =  new Dictionary<int, string>
    {
        {1, "MON" }, {2, "TUE" }, {3, "WED" }, {4, "THU"}, {5, "FRI"}, {6, "SAT"}, {0, "SUN"}
    };


    private int[] Time(float currentTime)
    {
        int[] time = new int[2];
        time[0] = (int) currentTime / 60;  //hour
        time[1] = (int) currentTime % 60 / 10;
        return time;
    }

    public void ChangeDisplay(float time, float day, int today, float money)
    {
        string min = Time(time)[1].ToString();
        TimeText.text = string.Format("{0:00} : ", Time(time)[0]) + min.PadRight(2, '0');
        DayText.text = "Day" + day.ToString();
        Money.text = money.ToString();
        TodayText.text = Today[today];
    }
    private void Start()
    {
        AddObserver(GameData.Instance);
    }
    private void OnDisable()
    {
        RemoveObserver(GameData.Instance);
    }

    public void AddObserver(IGameDataOb o)
    {
        o.AddObserver(this);
    }

    public void RemoveObserver(IGameDataOb o)
    {
        if(o != null) { o.RemoveObserver(this); }
    }

    public void Change(GameData obj)
    {
        if (obj is GameData)
        {
            var GameData = obj;
            ChangeDisplay(GameData.TimeOfDay, GameData.Day, GameData.Today, GameData.Money);
        }
    }
}
