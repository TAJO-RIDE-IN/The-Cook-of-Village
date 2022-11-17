using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PlayControl : MonoBehaviour
{
    public Button ContinueButton;
    public Button NewPlayButton;
    public GameObject ContinueUI;
    private PlayerData playerData;
    private GameData gameData;
    private void Start()
    {
        playerData = PlayerData.Instance;
        gameData = GameData.Instance;
        State();
    }
    private void State()
    {
        ContinueButton.interactable = (playerData.playerInfos.Count > 0);
        NewPlayButton.interactable = playerData.CanAddData();
    }
    public void NewPlayClick()
    {
        if (playerData.CanAddData())
        {
            playerData.AddPlayer();
        }
        State();
    }
    public void ContinueClick()
    {
        ContinueUI.SetActive(!ContinueUI.activeSelf);
    }

    public void StartGame(int PlayID)
    {
        gameData.PlayerID = PlayID;
        gameData.LoadDataTime(PlayID.ToString());
    }
}
