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

    private int[] Time(float currentTime)
    {
        int[] time = new int[2];
        time[0] = (int) currentTime / 60;  //hour
        time[1] = (int) currentTime % 60 / 10;
        return time;
    }

    public void ChangeDisplay(float time, float day, int money)
    {
        string min = Time(time)[1].ToString();
        TimeText.text = string.Format("{0:00} : ", Time(time)[0]) + min.PadRight(2, '0');


        //TimeText.text = Time(time)[0] + " : " + Time(time)[1]+"0";
        DayText.text = "Day" + day.ToString();
        Money.text = money.ToString();
        TodayText.text = Today(day);
    }

    private string Today(float day)
    {
        float today = day % 7;
        switch(today)
        {
            case 1:
                return "MON";
            case 2:
                return "TUE";
            case 3:
                return "WED";
            case 4:
                return "THU";
            case 5:
                return "FRI";
            case 6:
                return "SAT";
            case 0:
                return "SUN";
            default:
                return "";
        }
    }

    public void AddObserver(IGameDataOb o)
    {
        GameData.Instance.AddObserver(this);
    }

    public void RemoveObserver(IGameDataOb o)
    {
        GameData.Instance.RemoveObserver(this);
    }

    public void Change(GameData obj)
    {
        if (obj is GameData)
        {
            var GameData = obj;
            ChangeDisplay(GameData.TimeOfDay, GameData.Day, GameData.Money);
        }
    }
}
