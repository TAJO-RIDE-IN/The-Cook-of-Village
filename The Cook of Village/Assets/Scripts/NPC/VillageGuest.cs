using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class VillageGuest : MonoBehaviour
{
    public NPCInfos npcInfos;
    public void Awake()
    {
        npcInfos = NPCData.Instance.npcInfos[(int)npcInfos.work];
    }
}
