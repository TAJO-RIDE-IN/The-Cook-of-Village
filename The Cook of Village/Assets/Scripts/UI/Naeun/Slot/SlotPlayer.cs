using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class SlotPlayer : Slot<PlayerInfos>
{
    public PlayerInfos playerInfos
    {
        get { return Infos; }
        set 
        { 
            Infos = value;
            ModifySlot();
        }
    }
    public Text PlayerNameText;
    public Text RestaurantNameText;
    public Text DayText;
    public Text MoneyText;
    public DeleteUI deleteUI;
    public PlayControl playControl;

    private void OnDisable()
    {
        ObjectPooling<SlotPlayer>.ReturnObject(this);
    }

    public override void ModifySlot()
    {
        PlayerNameText.text = playerInfos.PlayerName;
        RestaurantNameText.text = playerInfos.RestaurantName;
        DayText.text = "Day " + playerInfos.Day + "ÀÏÂ÷";
        MoneyText.text = playerInfos.Money + "¿ø";
    }
    public void DeleteData()
    {
        playControl.WantDeleteData = playerInfos;
        deleteUI.playControl = playControl;
        deleteUI.DeleteUIState(true);
    }
    public override void SelectSlot()
    {
        playControl.ContinueGame(playerInfos);
    }
}
