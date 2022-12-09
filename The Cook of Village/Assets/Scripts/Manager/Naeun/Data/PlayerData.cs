using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using UnityEngine;

public class PlayerData : DataManager<PlayerData>
{
    public List<PlayerInfos> playerInfos = new List<PlayerInfos>();
    private GameData gameData;
    private string FilePath;
    protected override void Init()
    {
        FilePath = Application.persistentDataPath + "/PlayerData";
        LoadPlayerData();
    }
    private void Start()
    {
        gameData = GameData.Instance;
        gameData.ResetData(); 
    }
    private string CreateID()
    {
        DateTime currentTime = DateTime.Now;
        string yymmdd = currentTime.ToString("yyyyMMdd");
        string hhmmss = currentTime.ToString("HHmmss");
        return yymmdd + hhmmss;
    }
    /// <summary>
    /// PlayerData ���� ���� �����͸� ã�� �ҷ��´�.
    /// </summary>
    private void LoadPlayerData()
    {
        playerInfos.Clear();
        if (Directory.Exists(FilePath))
        {
            DirectoryInfo fileinfo = new DirectoryInfo(FilePath);
            DirectoryInfo[] files = fileinfo.GetDirectories().OrderBy(p => p.LastWriteTime).ToArray(); //������ ��¥������ ����
            foreach(var file in fileinfo.GetDirectories())
            {
                if(file.Name != "Default" || file.Name != "_0")
                {
                    GameInfos infos = new GameInfos();
                    LoadData(ref infos, "GameData", file.Name);
                    playerInfos.Add(infos.playerInfos);
                }
            }
        }
    }
    public void AddNewPlayer(PlayerInfos info)
    {
        gameData.PlayerID = CreateID();
        gameData.PlayerName = info.PlayerName;
        gameData.RestaurantName = info.RestaurantName;
        gameData.SaveDataTime(gameData.PlayerName);
    }
    public void DeleteData(PlayerInfos info)
    {
        string PlayName = info.PlayerName + "_" + info.PlayerID;
        DirectoryInfo fileinfo = new DirectoryInfo(FilePath + "/" + PlayName);
        fileinfo.Delete(true);
        playerInfos.Remove(info);
    }
    public void ContinuePlayer(PlayerInfos info)
    {
        gameData.PlayerID = info.PlayerID;
        gameData.PlayerName = info.PlayerName;
        gameData.LoadDataTime("Load");
    }
    public override void SaveDataTime(string PlayName) //�����͸� �ҷ����⸸ ��.
    {
    }
}
