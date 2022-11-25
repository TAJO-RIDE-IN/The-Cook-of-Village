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
                if (GameManager.Instance.NextSceneIndex == 3)
                {
                    if (!FurniturePooling.Instance.furnitureInstallMode.isActive)
                    {
                        pauseUI.PauseUIState(true);
                    }
                }
                pauseUI.PauseUIState(true);
            }
            else
            {
                UIManager.RemoveUseEsc();
            }
            return;
        }

        if (Input.GetKeyDown(KeyCode.U))
        {
            //내 정보
        }
        if (Input.GetKeyDown(KeyCode.I))
        {
            //인벤토리
        }
        if (Input.GetKeyDown(KeyCode.L))
        {
            //호감도
        }
        if (Input.GetKeyDown(KeyCode.R))
        {
            //레시피
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
