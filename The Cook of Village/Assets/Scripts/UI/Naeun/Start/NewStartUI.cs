using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class NewStartUI : MonoBehaviour
{
    private PlayerInfos playerInfos = new PlayerInfos();
    public InputField PlayerNameInput;
    public InputField RestaurantNameInput;
    public PlayControl playControl;
    public void NewStartUIState()
    {
        this.gameObject.SetActive(!this.gameObject.activeSelf);
    }

    public void ChangeInfos()
    {
        playerInfos.PlayerName = PlayerNameInput.text;
        playerInfos.RestaurantName = RestaurantNameInput.text;
    }
    
    public void NewData()
    {
        if(!PlayerNameInput.text.Equals("")&& !RestaurantNameInput.text.Equals(""))
        {
            playControl.NewStartGame(playerInfos);
        }
    }
}
