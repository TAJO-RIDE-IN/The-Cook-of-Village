using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PauseUI : UIController
{
    public ChangeScene changeScene;
    public GameObject pauseUI;
    public GameObject OptionUI;
    public GameObject ExitUI;
    public MenuUI menuUI;

    public void PauseUIState(bool state)
    {
        pauseUI.SetActive(state);
        menuUI.MenuUIState(!state);
        OptionUI.SetActive(false);
        ExitUI.SetActive(false);
        if (GameManager.Instance != null)
        {
            GameManager.Instance.Pause(state);
        }
    }
    public void HomeButtonClick()
    {
        GameData.Instance.SaveDataTime("HomeSave");
        changeScene.MoveScene();
    }
    protected override void Disable()
    {
        PauseUIState(false);
    }
    public void OptionButtonClick()
    {
        OptionUI.SetActive(!OptionUI.activeSelf);
    }
    public void ExitButtonClick()
    {
        ExitUI.SetActive(!ExitUI.activeSelf);
    }
}
