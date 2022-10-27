using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class OpenDoor : MonoBehaviour
{
    public Animation _animation;
    public List<string> animArray;
    

    private void Start()
    {
        {
            animArray = new List<string>();
            AnimationArray();
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if(other.CompareTag("Player"))
        {
            
            _animation.Play(animArray[0]);
        }
    }

    public void AnimationArray()
    {
        foreach (AnimationState state in _animation)
        {
            animArray.Add(state.name); 
            
        }
    }
    
}
