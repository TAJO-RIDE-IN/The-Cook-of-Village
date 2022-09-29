using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Calendar : MonoBehaviour, IObserver<GameData>
{
    public Image[] DayImage;
    public NPCInfos[] NPCHoliday;

    private void Start()
    {
        AddObserver(GameData.Instance);
    }


    private void Dayhighlight(int date)
    {
        DayImage[date - 1].gameObject.SetActive(true);
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
        }    
    }
}
