using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class DeleteUI : MonoBehaviour
{
    public PlayControl playControl;
    public ContinueUI continueUI;
    public Text PlayerName;

    public void DeleteUIState(bool state)
    {
        this.gameObject.SetActive(state);
        if (state)
        {
            PlayerName.text = "\"" + playControl.WantDeleteData.PlayerName + "\"";
        }
    }
    public void DeleteData()
    {
        playControl.DeleteData();
        continueUI.LoadData();
        DeleteUIState(false);
    }
}
