using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PauseUI : UIController
{
    public GameObject pauseUI;
    public GameObject OptionUI;
    public GameObject ExitUI;
    public MenuUI menuUI;
    public void Pause()
    {
        menuUI.MenuUIDisable();
        pauseUI.SetActive(!pauseUI.activeSelf);
        OptionUI.SetActive(false);
        ExitUI.SetActive(false);
        GameManager.Instance.Pause(pauseUI.activeSelf);
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
