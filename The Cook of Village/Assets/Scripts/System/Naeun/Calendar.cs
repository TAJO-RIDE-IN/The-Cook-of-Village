using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Calendar : MonoBehaviour, IObserver<GameData>
{
    public Image[] DayImage;
    public Image YesterDayImage;
    public Text MonthText;
    private void Start()
    {
        AddObserver(GameData.Instance);
    }
    private void OnEnable()
    {
        Dayhighlight(GameData.Instance.Date);
        MonthText.text = GameData.Instance.Month.ToString() + "월";
    }
    private void Dayhighlight(int date)
    {
        if(YesterDayImage != null) { YesterDayImage.enabled = false; }
        DayImage[date - 1].enabled = true;
        YesterDayImage = DayImage[date - 1];
    }
    public void AddObserver(IGameDataOb o)
    {
        o.AddObserver(this);
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
