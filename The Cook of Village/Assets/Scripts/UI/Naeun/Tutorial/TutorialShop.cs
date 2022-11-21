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
        for (int i = 0; i < EventButton.Length; i++) //클릭 이벤트 할당
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
    private void SliderChange(float _value) //Slider 값 바뀐경우 다음 이벤트
    {
        NextEvent(1);
        EventSlider.onValueChanged.RemoveListener(SliderChange); //한번 이벤트 실행한 후 삭제
    }
    private void NextEvent(int i) //다음 클릭 이미지 활성화
    {
        if (i.Equals(3)) //exit버튼 누를경우 다음 대사
        {
            Controller.VillageTutorialUI.DialogueText();
            return;
        }
        ClickImage[i].SetActive(false); //이전 이미지, 버튼 비활성화
        ClickImage[i + 1].SetActive(true); //다음 이미지, 버튼 활성화
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
