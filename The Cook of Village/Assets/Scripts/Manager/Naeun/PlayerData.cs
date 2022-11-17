using System;
using System.Collections.Generic;

public class PlayerData : DataManager<PlayerData>
{
    public List<PlayerInfos> playerInfos = new List<PlayerInfos>();
    public Dictionary<string, string> PlayNum = new Dictionary<string, string>();
    private GameData gameData;
    protected override void Init()
    {
        LoadPlayerData();
    }
    private void Start()
    {
        gameData = GameData.Instance;
        gameData.ResetData(); 
    }
    private void LoadPlayerData()
    {
        playerInfos.Clear();
        int count = 0;
        while (FileExists("GameData" + count))
        {
            GameInfos gameInfos = new GameInfos();
            LoadData(ref gameInfos, "GameData" + count);
            playerInfos.Add(gameInfos.playerInfos);
            count++;
        }
    }
    public bool CanAddData()
    {
        return (playerInfos.Count < 9);
    }
    public void AddPlayer()
    {
        if (CanAddData())
        {
            gameData.PlayerID = playerInfos.Count;
            gameData.SaveDataTime((playerInfos.Count).ToString());
        }
    }
    public void RemoveData(int PlayerID)
    {

    }
    public void ContinuePlayer(string PlayNum)
    {
        gameData.LoadDataTime(PlayNum);
    }
    public override void SaveDataTime(string PlayNum) //데이터를 불러오기만 함.
    {
    }
}
