using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CookPosition : MonoBehaviour
{
    public CookPositionUI cookPositionUI;
    private bool isPlayer;

    private void Update()
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
    }
    
    public void isUsedOrNot()
    {
        if (!ToolPooling.Instance.toolInstallMode.isUsed[cookPositionUI.index])
        {
            
        }
    }
    
    

    private void OnTriggerEnter(Collider other)
    {
        if(other.CompareTag("Player"))
        {
            isPlayer = true;
        }
    }
    private void OnTriggerExit(Collider other)
    {
        if(other.CompareTag("Player"))
        {
            isPlayer = false;
            cookPositionUI.gameObject.SetActive(false);
        }
    }
}
