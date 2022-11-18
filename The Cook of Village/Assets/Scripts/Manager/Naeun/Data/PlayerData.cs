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
    /// PlayerData 폴더 안의 데이터를 찾고 불러온다.
    /// </summary>
    private void LoadPlayerData()
    {
        playerInfos.Clear();
        if (Directory.Exists(FilePath))
        {
            DirectoryInfo fileinfo = new DirectoryInfo(FilePath);
            DirectoryInfo[] files = fileinfo.GetDirectories().OrderBy(p => p.LastWriteTime).ToArray(); //수정한 날짜순으로 정렬
            foreach(var file in fileinfo.GetDirectories())
            {
                if(file.Name != "Default" && file.Name != "플레이어 이름_0")
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
    public override void SaveDataTime(string PlayName) //데이터를 불러오기만 함.
    {
    }
}
