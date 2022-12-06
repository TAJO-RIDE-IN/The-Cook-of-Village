using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InputManager : MonoBehaviour
{
    public MenuUI menuUI;
    public PauseUI pauseUI;
    public StateUI stateUI;
    public InventoryUI inventoryUI;
    public LikeabliltyUI likeabliltyUI;
    public RecipeUI recipeUI;


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
                        Debug.Log("isActive false임");
                        pauseUI.PauseUIState(true);
                        return;
                    }
                    return;
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
            stateUI.StateUIState();
        }
        if (Input.GetKeyDown(KeyCode.I))
        {
            inventoryUI.InventoryState();
        }
        if (Input.GetKeyDown(KeyCode.L))
        {
            likeabliltyUI.LikeabliltyUIState();
        }
        if (Input.GetKeyDown(KeyCode.R))
        {
            recipeUI.RecipeUIState();
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
