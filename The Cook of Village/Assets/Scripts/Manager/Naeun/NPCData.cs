using System.Collections;
using System.Collections.Generic;
using UnityEngine;
[System.Serializable]
public class NPCInfos
{
    public enum Work { FruitShop, VegetableShop, MeetShop, ChocolateShop, PotionShop, interiorShop, Bank}
    [SerializeField] public Work work;
    public string Name;
    public int OpenTime;
    public int CloseTime;
    public int Holiday;
    public int likeability;
    public Sprite ProfileImage;
    public GameObject NPCModel;
}

public class NPCData : DataManager
{
    #region Singleton, LoadData
    private static NPCData instance = null;
    private void Awake() //�� ���۵ɶ� �ν��Ͻ� �ʱ�ȭ
    {
        if (null == instance)
        {
            instance = this;
            //LoadData<NPCInfos>(ref npcInfos, "NPCData"); //data �ϼ� �Ǿ����� �ٽ� Ȱ��ȭ
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
}
