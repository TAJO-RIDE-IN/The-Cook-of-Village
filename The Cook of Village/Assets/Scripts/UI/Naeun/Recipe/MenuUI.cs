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
    public Button Likeability;

    public void MenuUIState(bool state)
    {
        this.GetComponent<Button>().enabled = state;
        Recipe.enabled = state;
        Storage.enabled = state;
        Likeability.enabled = state;
    }
    public void ClickMenu()
    {
        onMenu = !onMenu;
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
