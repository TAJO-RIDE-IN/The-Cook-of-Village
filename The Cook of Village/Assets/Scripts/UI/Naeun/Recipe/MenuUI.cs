using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class MenuUI : MonoBehaviour
{
    private bool onMenu = false;
    public Animator SubMenuAni;
    public Toggle Recipe;
    public Toggle Storage;
    public Toggle Likeability;

    public void MenuUIDisable()
    {
        this.GetComponent<Button>().enabled = false;
        Recipe.enabled = false;
        Storage.enabled = false;
        Likeability.enabled = false;
    }
    public void MenuUIEnabled()
    {
        this.GetComponent<Button>().enabled = true;
        Recipe.enabled = true;
        Storage.enabled = true;
        Likeability.enabled = true;
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
