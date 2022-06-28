using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.UI;

public class Refrigerator : MonoBehaviour
{
    public RefrigeratorUI refrigeratorUI;
    private Animator frigdeAnimator;

    private void Start()
    {
        frigdeAnimator = transform.GetComponent<Animator>();
    }

    public void OpenRefrigerator()
    {
        frigdeAnimator.SetBool("isOpen", true);
        refrigeratorUI.OpenUI();
    }
    public void CloseRefrigerator()
    {
        frigdeAnimator.SetBool("isOpen", false);
        refrigeratorUI.CloseUI();
    }
}