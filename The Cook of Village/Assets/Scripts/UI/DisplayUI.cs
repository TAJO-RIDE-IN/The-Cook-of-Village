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
        time[0] = (int)Math.Truncate(currentTime);  //hour
        float minute = (float)Math.Truncate((currentTime - time[0]) * 100) / 100; //minute �Ҽ��� 2���� �ڸ�
        minute = (int)Math.Truncate(minute * 60);
        time[1] = (int)minute;
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
