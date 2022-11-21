using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ExitUI : MonoBehaviour
{
    public void ExitUIState()
    {
        this.gameObject.SetActive(!this.gameObject.activeSelf);
    }
    public void ExitGame()
    {
        GameManager.Instance.GameQuit();
    }
}
