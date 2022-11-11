using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CookPositionUI : MonoBehaviour
{
    public int index;

    public GameObject InstallButton;
    public GameObject NoCookTool;

    
    
    public void DirectSetUp()
    {
        ToolPooling.Instance.toolInstallMode.DirectInstall();
        ToolPooling.Instance.indexToChange = index;
        gameObject.SetActive(false);
    }

    public void CloseUI()
    {
        gameObject.SetActive(false);
    }
}
