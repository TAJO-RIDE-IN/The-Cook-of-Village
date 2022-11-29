using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class CalendarUI : UIController, IObserver<GameData>
{
    public Image[] DayImage;
    public Image YesterDayImage;
    public Text MonthText;
    private void Start()
    {
        AddObserver(GameData.Instance);
    }
    public void CalendarUIState(bool state)
    {
        this.gameObject.SetActive(state);
        if(state)
        {
            Init();
        }
    }
    private void Init()
    {
        foreach(var image in DayImage)
        {
            image.gameObject.SetActive(false);
        }
        Dayhighlight(GameData.Instance.Date);
        MonthText.text = GameData.Instance.Month.ToString() + "월";
    }
    private void Dayhighlight(int date)
    {
        if(YesterDayImage != null) { YesterDayImage.gameObject.SetActive(false); }
        DayImage[date - 1].gameObject.SetActive(true);
        YesterDayImage = DayImage[date - 1];
    }
    public void AddObserver(IGameDataOb o)
    {
        o.AddDayObserver(this);
    }

    public void Change(GameData obj)
    {
        if(obj is GameData)
        {
            Dayhighlight(obj.Date);
            MonthText.text = obj.Month.ToString() + "월";
        }    
    }
}
