using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CookPosition : MonoBehaviour
{
    public CookPositionUI CookPositionUI;
    private bool isPlayer;

    private void Update()
    {
        if (isPlayer)
        {
            if (Input.GetKeyDown(KeyCode.Space))
            {
                if (!ToolPooling.Instance.toolInstallMode.isUsed[CookPositionUI.index])
                {
                    CookPositionUI.gameObject.SetActive(true);
                }
                
            }
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
            CookPositionUI.gameObject.SetActive(false);
        }
    }
}
