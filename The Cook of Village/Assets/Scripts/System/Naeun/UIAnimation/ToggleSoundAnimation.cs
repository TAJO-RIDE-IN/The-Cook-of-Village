using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class ToggleSoundAnimation : UISoundAnimation, IPointerExitHandler
{
    public enum UIAnimation { Basics, Down, Right };
    public UIAnimation AnimationType = UIAnimation.Basics;
    private Dictionary<UIAnimation, Vector3> TypeVector = new Dictionary<UIAnimation, Vector3>();
    private Toggle toggle;
    private Vector3 DefaultVector;
    private Vector3 DownVector;
    private Vector3 RightVector;
    public override void Init()
    {
        isBasics = AnimationType.Equals(UIAnimation.Basics);
        toggle = this.GetComponent<Toggle>();
        AddDictionary();
    }
    private void AddDictionary()
    {
        DefaultVector = this.gameObject.transform.localPosition;
        DownVector = new Vector3(DefaultVector.x, DefaultVector.y - 20f, DefaultVector.z);
        RightVector = new Vector3(DefaultVector.x + 30f, DefaultVector.y, DefaultVector.z);
        TypeVector.Add(UIAnimation.Down, DownVector);
        TypeVector.Add(UIAnimation.Right, RightVector);
    }
    public override void OnPointerEnter(PointerEventData eventData)
    {
        if (CanPointer())
        {
            sound.Play(sound._audioClips["ButtonEnter"]);
            if(!isBasics)
            {
                UIManager.UIMoveAnimation(thisObject, TypeVector[AnimationType]);
            }
        }
    }
    public void OnPointerExit(PointerEventData eventData)
    {
        if (CanPointer() && !toggle.isOn)
        {
            if (!isBasics)
            {
                UIManager.UIMoveAnimation(thisObject, DefaultVector);
            }
        }
    }
    public override void OnPointerClick(PointerEventData eventData)
    {
        if (CanPointer())
        {
            sound.Play(sound._audioClips["ButtonClick"]);
        }
    }
    public override bool CanPointer()
    {
        return toggle.interactable && toggle.enabled;
    }
    public override void PlayAnimation()
    {
        if (toggle.isOn) //play
        {
            Vector3 vector = TypeVector[AnimationType];
            UIManager.UIMoveAnimation(thisObject, vector);
        }
        else 
        {
            UIManager.UIMoveAnimation(thisObject, DefaultVector);
        }
    }
}
