using System;
using System.Collections.Generic;

[System.Serializable]
public class PlayerInfos
{
    public GameInfos gameInfos;
    public int Money;
    public PlayerInfos(GameInfos Infos, int money)
    {
        Infos = gameInfos;
        money = Money;
    }
}

public class PlayerData : DataManager<PlayerData>
{
    public List<PlayerInfos> playerInfos = new List<PlayerInfos>();
    protected override void Init()
    {
        LoadPlayerData();
    }
    private void LoadPlayerData()
    {
        playerInfos = new List<PlayerInfos>();
        int count = 0;
        while (FileExists("GameInfos" + count))
        {
            GameInfos infos = new GameInfos();
            MoneyInfos moneyInfos = new MoneyInfos();
            LoadData(ref infos, "GameData" + count);
            LoadData(ref moneyInfos, "MoneyData" + count);
            playerInfos.Add(new PlayerInfos(infos, moneyInfos.Money));
            count++;
        }
    } //gameData.instance쓰기
    public bool CanAddData()
    {
        return (playerInfos.Count < 9);
    }
    public void AddPlayer()
    {
        if (CanAddData())
        {
            playerInfos.Add(new PlayerInfos(new GameInfos(), 0));
            SaveDataTime(0);
        }
    }

    public override void SaveDataTime(int PlayNum) //데이터를 불러오기만 함.
    {
    }
}
