using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Calendar : MonoBehaviour, IObserver<GameData>
{
    public GameObject[] DaySlot;
    public VillageNPC[] NPCHoliday;


    public void Change(GameData obj)
    {
        if(obj is GameData)
        {
            
        }    
    }
}
