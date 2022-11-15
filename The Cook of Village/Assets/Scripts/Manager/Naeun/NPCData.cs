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

public class NPCData : DataManager
{
    #region Singleton, LoadData
    private static NPCData instance = null;
    private void Awake() //씬 시작될때 인스턴스 초기화
    {
        if (null == instance)
        {
            instance = this;
            //LoadData<NPCInfos>(ref npcInfos, "NPCData"); //data 완성 되었을때 다시 활성화
        }
        else
        {
            Destroy(this.gameObject);
        }
    }
    public static NPCData Instance
    {
        get
        {
            if (null == instance)
            {
                return null;
            }
            return instance;
        }
    }
    #endregion
    [SerializeField] public NPCInfos[] npcInfos;

    public override void SaveDataTime()
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
