using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PlayControl : MonoBehaviour
{
    public Button ContinueButton;
    public Button NewPlayButton;
    public ChangeScene changeScene;
    public PlayerInfos WantDeleteData;
    private PlayerData playerData;
    private void Start()
    {
        playerData = PlayerData.Instance;
        State();
    }
    private void State()
    {
        ContinueButton.interactable = (playerData.playerInfos.Count > 0);
    }
    public void DeleteData()
    {
        playerData.DeleteData(WantDeleteData);
        WantDeleteData = null;
        State();
    }
    public void NewStartGame(PlayerInfos info)
    {
        GameManager.Instance.gameMode = GameManager.GameMode.Default;
        playerData.AddNewPlayer(info);
        changeScene.MoveScene();
        State();
    }
    public void ContinueGame(PlayerInfos info)
    {
        GameManager.Instance.gameMode = GameManager.GameMode.Default;
        playerData.ContinuePlayer(info);
        changeScene.MoveScene();
    }
}
