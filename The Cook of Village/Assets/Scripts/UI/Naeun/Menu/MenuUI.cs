using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class MenuUI : MonoBehaviour
{
    private bool onMenu = false;
    public Animator SubMenuAni;
    public Button[] MenuButton;

    public void MenuUIState(bool state)
    {
        foreach(var _button in MenuButton)
        {
            _button.enabled = state;
        }    
    }
    public void ClickMenu()
    {
        onMenu = !onMenu;
        if (onMenu == true)
        {
            SubMenuAni.SetTrigger("Open");
            return;
        }
        else
        {
            SubMenuAni.SetTrigger("Close");
        }
    }
}
