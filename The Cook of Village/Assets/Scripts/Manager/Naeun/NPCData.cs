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
    public int likeability;
    public int FavoriteFood;
    public bool EatFavriteFood;
    public bool VisitRestaurant;//하루마다 초기화
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

    public void ResetData()
    {
        foreach(var npc in npcInfos)
        {
            npc.VisitRestaurant = false;
        }
    }
}
