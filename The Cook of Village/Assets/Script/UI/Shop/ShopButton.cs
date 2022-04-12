/////////////////////////////////////
/// 학번 : 91914200
/// 이름 : JungNaEun 정나은
////////////////////////////////////
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class ShopButton : MonoBehaviour, IPointerDownHandler, IPointerUpHandler
{
    public Slider CountSlider;
    private bool IsClick = false;
    private bool changeValue = false;
    private bool ChangeValue
    {
        get { return changeValue; }
        set
        {
            changeValue = value;
            float speed = 0.2f;

            if (changeValue)
            {
                StartCoroutine(LongClickValueChange(speed));
            }
        }
    }
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

    public void OnPointerDown(PointerEventData eventData) //클릭감지
    {
        IsClick = true;
        StartCoroutine(DelayCheck(ChangeValue, 1f, (value) => ChangeValue = value));
    }
    public void OnPointerUp(PointerEventData eventData)
    {
        IsClick = false;
        ChangeValue = false;
    }

    public IEnumerator DelayCheck(bool CurreuntBool, float delay, Action<bool> makeResult)
    { 
        yield return new WaitForSeconds(delay);
        if (IsClick)
        {
            makeResult(true);
        }
    }

    public void ValueButtonClick()
    {
        string currentButton = EventSystem.current.currentSelectedGameObject.name;
        if (currentButton == "MinusButton")
        {
            plus = false;
            ChangeSliderValue(-1);
        }
        else if (currentButton == "PlusButton")
        {
            plus = true;
            ChangeSliderValue(1);
        }
    }
    private void ChangeSliderValue(int value)
    {
        CountSlider.value += value;
    }
}
