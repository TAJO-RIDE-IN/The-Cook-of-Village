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

    private bool isUsing;
    

    private void Start()
    {
        frigdeAnimation = transform.GetComponent<Animation>();
    }

    public void FridgeAnimaion(bool state)
    {
        if (state)
        {
            frigdeAnimation.Play("FridgeOpen");
            return;
        }
        frigdeAnimation.Play("FridgeClose");
        
    }
    public void UseRefrigerator(bool state)
    {
        if (state)
        {
            isUsing = true;
            FridgeAnimaion(state);
            FridgeUI.FridgeUIState(state);
            FridgeUI.fridge = this;
            string open = (state) ? " Open" : " Close";
            SoundManager.Instance.Play(SoundManager.Instance._audioClips["Refrigerator" + open]);
        }
        else
        {
            if (isUsing)
            {
                isUsing = false;
                FridgeAnimaion(state);
                FridgeUI.FridgeUIState(state);
                FridgeUI.fridge = this;
                string open = (state) ? " Open" : " Close";
                SoundManager.Instance.Play(SoundManager.Instance._audioClips["Refrigerator" + open]);
            }
        }
        
    }
}
