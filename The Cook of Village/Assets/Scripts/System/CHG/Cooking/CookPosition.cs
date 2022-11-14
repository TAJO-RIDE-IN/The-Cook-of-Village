using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CookPosition : MonoBehaviour
{
    public CookPositionUI cookPositionUI;
    public bool isPlate;

    /*private void Update()
    {
        if (isPlayer)
        {
            if (Input.GetKeyDown(KeyCode.Space))
            {
                if (!ToolPooling.Instance.toolInstallMode.isUsed[cookPositionUI.index])
                {
                    cookPositionUI.gameObject.SetActive(true);
                }
                
            }
        }
    }*/
    
    public void isUsedOrNot()
    {
        if (!ToolPooling.Instance.toolInstallMode.isUsed[cookPositionUI.index])
        {
            
        }
    }
    
    

}
