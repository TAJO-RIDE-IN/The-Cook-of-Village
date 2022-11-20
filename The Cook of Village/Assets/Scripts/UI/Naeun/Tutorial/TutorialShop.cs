using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.UI;

public class TutorialShop : MonoBehaviour
{
    public List<GameObject> ClickImage = new List<GameObject>();
    public List<Button> ShopButton = new List<Button>();
    public Button[] EventButton;
    public Slider EventSlider;
    public Toggle Resell;
    public GameObject ClickBlock;
    public TutorialVillageController Controller;

    public void Start()
    {
        if(GameManager.Instance.gameMode == GameManager.GameMode.Tutorial)
        {
            Controller.VillageTutorialUI.DialogueText();
            Init();
        }
    }

    private void Init()
    {
        foreach(var button in ShopButton)
        {
            button.interactable = false;
        }
        foreach (var image in ClickImage)
        {
            image.SetActive(false);
        }
        for (int i = 0; i < EventButton.Length; i++) //Ŭ�� �̺�Ʈ �Ҵ�
        {
            int index = i;
            if(EventButton[i] != null)
            {
                EventButton[i].onClick.AddListener(() => NextEvent(index));
            }
        }
        EventSlider.onValueChanged.AddListener(SliderChange);
        Resell.interactable = false;
        ClickImage[0].SetActive(true);
        ClickAnimation(0);
        ClickBlock.SetActive(true);
    }
    private void ClickAnimation(int index)
    {
        LeanTween.scale(ClickImage[index], new Vector3(1.3f, 1.3f, 1.3f), 0.5f).setLoopPingPong();
    }
    private void SliderChange(float _value) //Slider �� �ٲ��� ���� �̺�Ʈ
    {
        NextEvent(1);
        EventSlider.onValueChanged.RemoveListener(SliderChange); //�ѹ� �̺�Ʈ ������ �� ����
    }
    private void NextEvent(int i) //���� Ŭ�� �̹��� Ȱ��ȭ
    {
        if (i.Equals(3)) //exit��ư ������� ���� ���
        {
            Controller.VillageTutorialUI.DialogueText();
            return;
        }
        ClickImage[i].SetActive(false); //���� �̹���, ��ư ��Ȱ��ȭ
        ClickImage[i + 1].SetActive(true); //���� �̹���, ��ư Ȱ��ȭ
        ClickAnimation(i+1);
        if (EventButton[i] != null)
        {
            EventButton[i].interactable = false;
        }
        if (EventButton[i + 1] != null)
        {
            EventButton[i + 1].interactable = true;
        }
        ClickBlock.SetActive(!i.Equals(0));
    }
}
