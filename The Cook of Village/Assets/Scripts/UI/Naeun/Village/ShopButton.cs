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
    private bool IsClick = false;

    private bool plus = false;

    Dictionary<bool, int> SliderValue = new Dictionary<bool, int>();
    
    void Start()
    {
        SliderValue.Add(true, 1);
        SliderValue.Add(false, -1);
    }
    private IEnumerator LongClickValueChange(float speed)
    {
        while(IsClick)
        {
            ChangeSliderValue(SliderValue[plus]);
            yield return new WaitForSeconds(speed);
        }
    }

    public void OnPointerDown(PointerEventData eventData) // 클릭을 하는 순간 실행
    {
        IsClick = true;
        StartCoroutine(ChangeWithDelay.CheckDelay(1f, () => StartCoroutine(LongClickValueChange(ClickSpeed))));
    }
    public void OnPointerUp(PointerEventData eventData) // 클릭을 떼는 순간 실행
    {
        IsClick = false;
    }


    public void ValueButtonClick(int count)
    {
        plus = (count > 0);
        ChangeSliderValue(count);
    }
    private void ChangeSliderValue(int value)
    {
        CountSlider.value += value;
    }
}
