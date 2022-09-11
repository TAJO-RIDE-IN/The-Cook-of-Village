using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InstallMode : MonoBehaviour
{
    public GameObject InstallUI;
    void Start()
    {
        
    }

    void Update()
    {
        
    }

    public void StartInstall()
    {
        GameManager.Instance.IsInstall = true;
        GameManager.Instance.Pause();
        InstallUI.SetActive(true);
    }
}
