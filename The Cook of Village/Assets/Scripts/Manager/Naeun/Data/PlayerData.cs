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
    private string CreateID() //플레이어의 ID를 생성한다.
    {
        DateTime currentTime = DateTime.Now;
        string yymmdd = currentTime.ToString("yyyyMMdd");
        string hhmmss = currentTime.ToString("HHmmss");
        return yymmdd + hhmmss;
    }
    /// <summary>
    /// PlayerData의 데이터를 불러온다.
    /// </summary>
    private void LoadPlayerData() //이어하기에 나타낼 데이터를 불러온다.
    {
        playerInfos.Clear();
        if (Directory.Exists(FilePath))
        {
            DirectoryInfo fileinfo = new DirectoryInfo(FilePath);
            DirectoryInfo[] files = fileinfo.GetDirectories().OrderBy(p => p.LastWriteTime).ToArray();
            foreach(var file in fileinfo.GetDirectories())
            {
                if(file.Name != "Default") //Default는 기본데이터이기 때문에 불어오지 않는다.
                {
                    GameInfos infos = new GameInfos();
                    LoadData(ref infos, "GameData", file.Name);
                    playerInfos.Add(infos.playerInfos);
                }
            }
        }
    }
    public void AddNewPlayer(PlayerInfos info) //새로운 플레이어 데이터를 생성한다.
    {
        gameData.PlayerID = CreateID();
        gameData.PlayerName = info.PlayerName;
        gameData.RestaurantName = info.RestaurantName;
        gameData.SaveDataTime(gameData.PlayerName);
    }
    public void DeleteData(PlayerInfos info) //컴퓨터에 저장된 플레이어 데이터를 삭제한다.
    {
        string PlayName = info.PlayerName + "_" + info.PlayerID;
        DirectoryInfo fileinfo = new DirectoryInfo(FilePath + "/" + PlayName);
        fileinfo.Delete(true);
        playerInfos.Remove(info);
    }
    public void ContinuePlayer(PlayerInfos info) //선택한 데이터로 바꾸어준다.
    {
        gameData.PlayerID = info.PlayerID;
        gameData.PlayerName = info.PlayerName;
        gameData.LoadDataTime("Load");
    }
    public override void SaveDataTime(string PlayName) //값을 저장하지 않는다.
    {
    }
}
