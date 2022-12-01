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
    public GameObject SaveUI;
    public GameObject SaveImage;
    public List<GameObject> SaveText = new List<GameObject>();
    private bool Am;
    private Dictionary<int, string> Today =  new Dictionary<int, string>
    {
        {1, "월요일" }, {2, "화요일" }, {3, "수요일" }, {4, "목요일"}, {5, "금요일"}, {6, "토요일"}, {0, "일요일"}
    };
    private Dictionary<bool, string> AmPm = new Dictionary<bool, string>
    {
        {true, "AM" }, {false, "PM" }
    };

    private int[] CurrentTime(float dataTime)
    {
        int[] time = new int[2];
        int hour = (int)dataTime / 60;
        Am = (hour < 12);
        hour = (hour > 12) ? hour-12 : hour;
        time[0] = hour;  //hour
        time[1] = (int) dataTime % 60 / 10;
        return time;
    }

    public void ChangeDisplay(float time, int month, int date, int today)
    {
        string min = CurrentTime(time)[1].ToString();
        TimeText.text = string.Format("{0:00} : ", CurrentTime(time)[0]) + min.PadRight(2, '0');
        AmPmText.text = AmPm[Am];
        DayText.text = month.ToString() + " 월 " + date.ToString() + " 일 ";
        Money.text = MoneyData.Instance.Money.ToString();
        TodayText.text = Today[today];
    }
    private bool PlaySaveAnimation = false;
    private IEnumerator SaveDataUI()
    {
        PlaySaveAnimation = true;
        int count = 0;
        LeanTween.rotateAround(SaveImage, Vector3.forward, -360, 1f).setEaseInQuad().setLoopCount(20);
        while (PlaySaveAnimation)
        {
            foreach (var text in SaveText)
            {
                LeanTween.moveLocalY(text, 10, 0.5f).setEasePunch().setIgnoreTimeScale(true);
                yield return new WaitForSeconds(0.8f);
            }
            if (count.Equals(3))
            {
                SaveUI.SetActive(false);
                PlaySaveAnimation = false;
            }
            count++;
        }
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
            if(!obj.UseSave && !PlaySaveAnimation)
            {
                SaveUI.SetActive(true);
                StartCoroutine(SaveDataUI());
            }    
            ChangeDisplay(GameData.TimeOfDay, GameData.Month, GameData.Date, GameData.Today);
        }
    }
}
