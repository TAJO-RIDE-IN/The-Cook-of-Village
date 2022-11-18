using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.UI;

public class ContinueUI : UIController
{
    public SlotPlayer[] PlayerSlot;
    public PlayControl playControl;
    private PlayerData playerData;
    public void ContinueUIState()
    {
        ResetSlot();
        playerData = PlayerData.Instance;
        this.gameObject.SetActive(!this.gameObject.activeSelf);
        if(this.gameObject.activeSelf)
        {
            LoadData();
        }
    }

    private void ResetSlot()
    {
        foreach(var slot in PlayerSlot)
        {
            slot.gameObject.SetActive(false);
        }
    }
    
    private void LoadData()
    {
        foreach(var infos in playerData.playerInfos.Select((value, index) => (value, index)))
        {
            PlayerSlot[infos.index].gameObject.SetActive(true);
            PlayerSlot[infos.index].playControl = playControl;
            PlayerSlot[infos.index].playerInfos = infos.value;
        }        
    }
}
