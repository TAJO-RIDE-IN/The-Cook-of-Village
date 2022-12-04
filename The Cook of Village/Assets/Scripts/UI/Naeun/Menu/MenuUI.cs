using UnityEngine;
using UnityEngine.UI;

public class MenuUI : MonoBehaviour
{
    private bool onMenu = false;
    public Animator SubMenuAni;
    public Button[] MenuButton;
    public void MenuUIState(bool state)
    {
        foreach (var _button in MenuButton)
        {
            _button.enabled = state;
        }
    }
    public void ClickMenu()
    {
        onMenu = !onMenu;
        if (onMenu.Equals(true))
        {
            UIManager.SubMenuChangeisUI(true);
            SubMenuAni.SetTrigger("Open");
            return;
        }
        else
        {
            UIManager.SubMenuChangeisUI(false);
            SubMenuAni.SetTrigger("Close");
        }
    }
}
