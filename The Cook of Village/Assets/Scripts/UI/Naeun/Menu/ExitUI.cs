using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ExitUI : MonoBehaviour
{
    public void ExitUIState()
    {
        this.gameObject.SetActive(!this.gameObject.activeSelf);
        if(this.gameObject.activeSelf)
        {
            UIManager.UIOpenScaleAnimation(this.gameObject);
        }
    }
    public void ExitGame()
    {
        GameManager.Instance.GameQuit();
    }
}
