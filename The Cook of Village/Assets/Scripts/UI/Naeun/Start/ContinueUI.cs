using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.UI;

public class ContinueUI : UIController
{
    public PlayControl playControl;
    private List<SlotPlayer> slotPlayer = new List<SlotPlayer>();
    private PlayerData playerData;
    public void ContinueUIState()
    {
        playerData = PlayerData.Instance;
        this.gameObject.SetActive(!this.gameObject.activeSelf);
        if(this.gameObject.activeSelf)
        {
            LoadData();
        }
    }

    private void ResetSlot()
    {
        if(playerData.playerInfos.Count == 0)
        {
            ContinueUIState();
        }
        foreach(var slot in slotPlayer)
        {
            ObjectPooling<SlotPlayer>.ReturnObject(slot);
        }
    }
    public void LoadData()
    {
        ResetSlot();
        foreach (var infos in playerData.playerInfos)
        {
            SlotPlayer slot = ObjectPooling<SlotPlayer>.GetObject();
            slot.gameObject.SetActive(true);
            slot.playControl = playControl;
            slot.playerInfos = infos;
            slotPlayer.Add(slot);
        }        
    }
}
