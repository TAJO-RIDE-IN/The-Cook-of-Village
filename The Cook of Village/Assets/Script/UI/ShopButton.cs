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
    private bool isClick = false;
    private bool IsClick
    {
        get { return isClick; }
        set 
        {
            Debug.Log("set");
            isClick = value;
            float speed = 0.5f;
            if(isClick == true)
            {
                Debug.Log("true");
                if (SpeedUPTime)
                {
                    speed = 0.1f;
                }
                LongClickValueChange(speed);  
            }
        }
    }
    private bool SpeedUPTime = false;
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
        StartCoroutine(IsClick.CheckDelay<bool>(true, 1f, (value) => IsClick = value));
        StartCoroutine(SpeedUPTime.CheckDelay<bool>(true, 2f, (value) => SpeedUPTime = value));
    }
    public void OnPointerUp(PointerEventData eventData)
    {
        IsClick = false;
        SpeedUPTime = false;
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
