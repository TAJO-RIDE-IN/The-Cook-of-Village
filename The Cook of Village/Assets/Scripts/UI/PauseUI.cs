using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PauseUI : MonoBehaviour
{
    public GameObject pauseUI;
    public GameObject OptionUI;
    public GameObject ExitUI;
    public void Pause()
    {
        GameManager.Instance.Pause();
        pauseUI.SetActive(!pauseUI.activeSelf);
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
