using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class MenuUI : MonoBehaviour
{
    private bool onMenu = false;
    public Animator SubMenuAni;
    public Button Recipe;
    public Button Storage;
    public void ClickMenu()
    {
        onMenu = !onMenu;
        Recipe.enabled = onMenu;
        Storage.enabled = onMenu;
        if (onMenu == true)
        {
            SubMenuAni.SetTrigger("Open");
        }
        else
        {
            SubMenuAni.SetTrigger("Close");
        }
    }
}
