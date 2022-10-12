using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class VillageGuest : GuestNPC, IObserver<GameData>
{
    public NPCInfos npcInfos;
    public bool RestaurantVisit;
    public void Start()
    {
        npcInfos = NPCData.Instance.npcInfos[(int)npcInfos.work];
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
            RestaurantVisit = false;
        }
    }
}
