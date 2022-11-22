using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class VillageGuest : GuestNPC
{
    public Color VillageFoodColor = new Color(0,0,0,0.7f);
    public NPCInfos npcInfos;
    public void Start()
    {
        npcInfos = NPCData.Instance.npcInfos[(int)npcInfos.work];
    }
    public void VisitRestaurant()
    {
        npcInfos.VisitRestaurant = true;
    }
}
