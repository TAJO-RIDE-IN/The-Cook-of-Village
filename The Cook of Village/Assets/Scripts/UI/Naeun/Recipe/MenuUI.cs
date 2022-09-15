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

    public void ButtonDisable()
    {
        this.GetComponent<Button>().enabled = false;
        Recipe.enabled = false;
        Storage.enabled = false;
    }
    public void ClickMenu()
    {
        onMenu = !onMenu;
        Recipe.enabled = onMenu;
        Storage.enabled = onMenu;
        if (onMenu == true)
        {
            GameManager.Instance.IsUI = true;
            SubMenuAni.SetTrigger("Open");
        }
        else
        {
            GameManager.Instance.IsUI = false;
            SubMenuAni.SetTrigger("Close");
        }
    }
}
