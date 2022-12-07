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
    private void ChangeSubMenuChangeisUI(bool state)
    {
        if(GameManager.Instance.CurrentSceneIndex.Equals(2))
        {
            UIManager.SubMenuChangeisUI(state);
        }
    }
    public void ClickMenu()
    {
        onMenu = !onMenu;
        ChangeSubMenuChangeisUI(onMenu);
        if (onMenu)
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
