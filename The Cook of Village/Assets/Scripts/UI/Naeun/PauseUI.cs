using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PauseUI : UIController
{
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
        GameManager.Instance.Pause(state);
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
