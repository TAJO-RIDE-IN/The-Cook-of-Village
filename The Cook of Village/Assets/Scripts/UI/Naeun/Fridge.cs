using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.UI;

public class Fridge : MonoBehaviour
{
    public FridgeUI refrigeratorUI;
    private Animator frigdeAnimator;

    private void Start()
    {
        frigdeAnimator = transform.GetComponent<Animator>();
    }

    public void FridgeAnimaion(bool state)
    {
        frigdeAnimator.SetBool("isOpen", state);
    }
    public void OpenRefrigerator()
    {
        FridgeAnimaion(true);
        refrigeratorUI.OpenUI();
    }
    public void CloseRefrigerator()
    {
        FridgeAnimaion(false);
        refrigeratorUI.CloseUI();
    }
}
