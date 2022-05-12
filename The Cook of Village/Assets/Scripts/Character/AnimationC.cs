using System.Collections;
using System.Collections.Generic;
using Cinemachine;
using UnityEngine;

public class AnimationC : MonoBehaviour
{
    protected Animator charAnimator;
    protected AnimatorOverrideController animatorOverrideController;
    public AnimationClip[] weaponAnimationClip;
    public object Play { get; set; }


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
