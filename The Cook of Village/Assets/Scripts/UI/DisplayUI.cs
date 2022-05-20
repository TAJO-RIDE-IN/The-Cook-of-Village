using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class DisplayUI : MonoBehaviour, IObserver<GameDataManager>
{
    public Text Money;
    public Text TimeText;
    public Text DayText;

    private int[] Time(float currentTime)
    {
        int[] time = new int[2];
        time[0] = (int) currentTime / 60;  //hour
        time[1] = (int) currentTime % 60;
        return time;
    }

    public void ChangeDisplay(float time, float day, int money)
    {
        TimeText.text = Time(time)[0] + " : " + Time(time)[1];
        DayText.text = "Day" + day.ToString();
        Money.text = money.ToString();
    }
    public void AddObserver(IGameDataOb o)
    {
        GameDataManager.Instance.AddObserver(this);
    }

    public void RemoveObserver(IGameDataOb o)
    {
        GameDataManager.Instance.RemoveObserver(this);
    }

    public void Change(GameDataManager obj)
    {
        if (obj is GameDataManager)
        {
            var GameData = obj;
            ChangeDisplay(GameData.TimeOfDay, GameData.Day, GameData.Money);
        }
    }
}
