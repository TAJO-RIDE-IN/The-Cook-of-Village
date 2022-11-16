using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
[System.Serializable]
public class NPCInfos
{
    public enum Work { FruitShop, VegetableShop, MeetShop, ChocolateShop, PotionShop, interiorShop, Bank}
    [SerializeField] public Work work;
    public string Name;
    public string KoreanName;
    public int OpenTime;
    public int CloseTime;
    public int Holiday;
    [Range (0, 500)]public int Likeability;
    public int FavoriteFood;
    public bool EatFavriteFood;
    public bool VisitRestaurant;//하루마다 초기화
    public bool VisitPlayer;//하루마다 초기화
    public Sprite ProfileImage;
    public GameObject NPCModel;
}

public class NPCData : DataManager<NPCData>
{
    [SerializeField] public NPCInfos[] npcInfos;

    public override void SaveDataTime(int PlayNum)
    {
        SaveArrayData<NPCInfos>(ref npcInfos, "NPCData");
    }
    public static Dictionary<NPCInfos.Work, ItemType.Type> WorkDataType = new Dictionary<NPCInfos.Work, ItemType.Type>()
    {
        {NPCInfos.Work.FruitShop, ItemType.Type.Fruit}, {NPCInfos.Work.VegetableShop, ItemType.Type.Vegetable}, {NPCInfos.Work.MeetShop, ItemType.Type.Meat},
        {NPCInfos.Work.ChocolateShop, ItemType.Type.Other},
        {NPCInfos.Work.PotionShop, ItemType.Type.Potion}, {NPCInfos.Work.interiorShop, ItemType.Type.CookingTool}
    };
    public float LikeabilityEffect(NPCInfos.Work work)
    {
        int likeability = npcInfos[(int)work].Likeability / 100;
        float shop = 1f;
        float bank = 0.04f;
        if(likeability == 5)
        {
            shop = 0.7f;
            bank = 0.10f;
        }
        else
        {
            shop = 1f - 0.05f * likeability;
            bank = 0.04f + 0.01f * likeability;
        }
        float effect = (work == NPCInfos.Work.Bank) ? bank : shop;
        return effect;
    }

    public int NPCShopPrice(NPCInfos.Work work, int price)
    {
        int modify = (int)Math.Round(price * LikeabilityEffect(work));
        return modify;
    }
    public void ChangeLikeability(NPCInfos.Work work, string situation)
    {
        int likeability = 0;
        if(situation == "ReceiveFood" && !npcInfos[(int)work].VisitRestaurant)
        {
            npcInfos[(int)work].VisitRestaurant = true;
            likeability = 20;
        }
        else if(situation == "PlayerUse" && !npcInfos[(int)work].VisitPlayer)
        {
            npcInfos[(int)work].VisitPlayer = true;
            likeability = 5;
        }
        npcInfos[(int)work].Likeability += likeability;
    }

    public void ResetData()
    {
        foreach(var npc in npcInfos)
        {
            npc.VisitRestaurant = false;
            npc.VisitPlayer = false;
        }
    }
}
