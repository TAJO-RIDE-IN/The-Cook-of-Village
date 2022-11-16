using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class ShopButton : MonoBehaviour, IPointerDownHandler, IPointerUpHandler
{
    public Slider CountSlider;
    public float ClickSpeed = 0.2f;
    public int ClickValue;
    private bool IsClick = false;
    private Coroutine ValueCoroutine; 
   
    private IEnumerator LongClickValueChange(float speed)
    {
        while(IsClick)
        {
            ChangeSliderValue(ClickValue);
            yield return new WaitForSeconds(speed);
        }
    }

    public void OnPointerDown(PointerEventData eventData) // 클릭을 하는 순간 실행
    {
        IsClick = true;
        ChangeSliderValue(ClickValue);
        if (ValueCoroutine != null)
        {
            StopCoroutine(ValueCoroutine);
        }
        ValueCoroutine = StartCoroutine(ChangeWithDelay.CheckDelay(1f, () => StartCoroutine(LongClickValueChange(ClickSpeed))));
    }
    public void OnPointerUp(PointerEventData eventData) // 클릭을 떼는 순간 실행
    {
        if(ValueCoroutine != null)
        {
            StopCoroutine(ValueCoroutine);
        }
        IsClick = false;
    }
    private void ChangeSliderValue(int value)
    {
        CountSlider.value += value;
    }
}
