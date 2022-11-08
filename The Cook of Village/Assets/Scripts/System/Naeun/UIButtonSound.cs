using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class UIButtonSound : MonoBehaviour, IPointerEnterHandler, IPointerClickHandler
{
    private SoundManager sound;
    private void Start()
    {
        sound = SoundManager.Instance;
    }
    public void OnPointerEnter(PointerEventData eventData) 
    {
        sound.Play(sound._audioClips["ButtonEnter"]);
    }
    public void OnPointerClick(PointerEventData eventData)
    {
        sound.Play(sound._audioClips["ButtonClick"]);
    }
}
