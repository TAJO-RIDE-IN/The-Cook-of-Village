using System.Collections;
using System.Collections.Generic;
using Cinemachine;
using UnityEngine;

public class Animation : MonoBehaviour
{
    protected Animator charAnimator;
    protected AnimatorOverrideController animatorOverrideController;
    public AnimationClip[] weaponAnimationClip;
    
    
    protected virtual void Start()
    {
        if (transform.GetComponent<Animator>() != null) 
        {
            charAnimator = transform.GetComponent<Animator>();
            animatorOverrideController = new AnimatorOverrideController(charAnimator.runtimeAnimatorController);
            charAnimator.runtimeAnimatorController = animatorOverrideController;
        }
        
    }

}
