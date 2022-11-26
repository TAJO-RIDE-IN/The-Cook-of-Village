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
    private SoundManager soundManager;
    public bool isUsing;

    private void Start()
    {
        frigdeAnimation = transform.GetComponent<Animation>();
        soundManager = SoundManager.Instance;
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
            FridgeUI.fridge = this;
        }
        FridgeUI.FridgeUIState(state);
        ChangeState(state);
    }
    public void ChangeState(bool state)
    {
        isUsing = state;
        FridgeAnimaion(state);
        string open = (state) ? " Open" : " Close";
        soundManager.Play(soundManager._audioClips["Refrigerator" + open]);
    }
}
