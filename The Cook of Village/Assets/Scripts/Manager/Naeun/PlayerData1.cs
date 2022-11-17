using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/*public class PlayerInfos
{
    public GameInfos gameInofs;
    public MoneyInfos moneyInfos;
}*/
public class PlayerData1 : DataManager<PlayerData1>
{
    private static List<PlayerInfos> playerList = new List<PlayerInfos>();
    public List<PlayerInfos> PlayerList
    {
        get { return playerList; }
        set
        {
            playerList = value;
            SaveDataTime(0);
        }
    }

/*    protected override void Init()
    {
        PlayerData[] array = playerList.ToArray();
        LoadArrayData(ref array, "PlayerData");
    }*/
    public override void SaveDataTime(int PlayNum)
    {
        PlayerInfos[] array = playerList.ToArray();
        SaveArrayData(ref array, "PlayerData");
    }
}
