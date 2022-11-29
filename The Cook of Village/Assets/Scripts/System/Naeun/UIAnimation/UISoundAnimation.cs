using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public abstract class UISoundAnimation : MonoBehaviour, IPointerEnterHandler, IPointerClickHandler
{
    protected GameObject thisObject;
    protected SoundManager sound;
    protected bool isBasics;
    private void Start()
    {
        sound = SoundManager.Instance;
    }
    private void Awake()
    {
        thisObject = this.gameObject;
        Init();
    }
    public abstract void Init();
    public virtual void OnPointerEnter(PointerEventData eventData)
    {
        if (CanPointer())
        {
            sound.Play(sound._audioClips["ButtonEnter"]);
            PlayAnimation();
        }
    }
    public virtual void OnPointerClick(PointerEventData eventData)
    {
        if (CanPointer())
        {
            sound.Play(sound._audioClips["ButtonClick"]);
            PlayAnimation();
        }
    }

    public abstract bool CanPointer();
    public abstract void PlayAnimation();
}
