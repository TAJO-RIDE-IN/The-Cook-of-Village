using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.UI;

public class TutorialShop : TutorialDetailsUI
{
    public List<Button> ShopButton = new List<Button>();
    public Slider EventSlider;
    public Toggle Resell;

    protected override void AddInit()
    {
        EventSlider.onValueChanged.AddListener(SliderChange);
        Resell.interactable = false;
    }
    protected override void AddEvent(int index)
    {
        ClickBlock.SetActive(!index.Equals(0));
    }
    private void SliderChange(float _value) //Slider �� �ٲ��� ���� �̺�Ʈ
    {
        NextEvent(1);
        EventSlider.onValueChanged.RemoveListener(SliderChange); //�ѹ� �̺�Ʈ ������ �� ����
    }
    protected override void EndEvent()
    {
        Controller.NextDialogue();
    }
}
