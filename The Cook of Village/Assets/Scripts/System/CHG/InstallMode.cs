using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InstallMode : MonoBehaviour
{
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
    }
}
