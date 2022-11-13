using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.UI;

public class Fridge : MonoBehaviour
{
    public FridgeUI FridgeUI;
    private Animation frigdeAnimation;
    

    private void Start()
    {
        frigdeAnimation = transform.GetComponent<Animation>();
    }

    public void FridgeAnimaion(bool state)
    {
        //frigdeAnimation.SetBool("isOpen", state);
    }
    public void UseRefrigerator(bool state)
    {
        FridgeAnimaion(state);
        FridgeUI.FridgeUIState(state);
    }
}
