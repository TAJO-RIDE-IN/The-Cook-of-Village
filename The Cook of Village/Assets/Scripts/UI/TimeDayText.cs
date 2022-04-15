using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class TimeDayText : MonoBehaviour
{
    public Text TimeText;
    public Text DayText;

    private int[] Time(float currentTime)
    {
        int[] time = new int[2];
        time[0] = (int)Math.Truncate(currentTime);  //hour
        float minute = (float)Math.Truncate((currentTime - time[0])*100)/100; //minute 소수점 2에서 자름
        minute = (int)Math.Truncate(minute * 60);
        time[1] = (int)minute;
        return time;
    }

    public void ChangeTime(float time)
    {
        TimeText.text = Time(time)[0] + " : " + Time(time)[1];
    }
    public void ChangeDay(float day)
    {
        DayText.text = "Day" + day.ToString();
    }
}
