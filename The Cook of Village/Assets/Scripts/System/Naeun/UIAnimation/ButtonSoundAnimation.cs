using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;
using System;

public class ButtonSoundAnimation : UISoundAnimation
{
    public enum UIAnimation { Basics, Scale};
    public UIAnimation AnimationType = UIAnimation.Basics;
    private Button button;

    public override void Init()
    {
        isBasics = AnimationType.Equals(UIAnimation.Basics);
        button = this.GetComponent<Button>();
    }
    public override bool CanPointer()
    {
        return button.interactable && button.enabled;
    }
    public override void PlayAnimation()
    {
        if(!isBasics)
        {
            UIManager.UIScalePunchAnimation(thisObject);
        }
    }
}
