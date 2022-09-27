using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Calendar : MonoBehaviour, IObserver<GameData>
{
    public Image[] DayImage;
    public VillageNPC[] NPCHoliday;

    private void Start()
    {
        AddObserver(GameData.Instance);
    }


    private void ChangeImage(int date)
    {
        foreach(var npc in NPCHoliday)
        {
            if(date == npc.CloseDay)
            {
                break;
            }
        }
    }
    public void AddObserver(IGameDataOb o)
    {
        o.AddDayObserver(this);
    }

    public void Change(GameData obj)
    {
        if(obj is GameData)
        {
            ChangeImage(obj.Date);
        }    
    }
}
