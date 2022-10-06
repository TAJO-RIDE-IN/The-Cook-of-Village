using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class VillageGuest : GuestNPC
{
    public NPCInfos npcInfos;
    public void Awake()
    {
        npcInfos = NPCData.Instance.npcInfos[(int)npcInfos.work];
    }
}
