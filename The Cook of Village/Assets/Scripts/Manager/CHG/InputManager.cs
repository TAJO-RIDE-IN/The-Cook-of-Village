using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InputManager : MonoBehaviour
{
    public MenuUI menuUI;
    public PauseUI pauseUI;

    
    private bool isPause;
    // Gamemanager.NextSceneIndex에 따라서 다르게 해줘야할듯
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            if(UIManager.UIObject.Count == 0)
            {
                pauseUI.PauseUIState(true);
            }
            else
            {
                UIManager.RemoveUseEsc();
            }
            return;
        }
        if (Time.timeScale != 0f)
        {
            if (Input.GetKeyDown(KeyCode.Tab))
            {
                menuUI.ClickMenu();
            
            
                return;
            }
        }
        

    }
}
