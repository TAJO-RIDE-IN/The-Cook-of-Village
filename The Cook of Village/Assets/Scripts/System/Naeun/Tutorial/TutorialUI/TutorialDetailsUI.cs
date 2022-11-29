using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UI;

public abstract class TutorialDetailsUI : MonoBehaviour
{
    public List<GameObject> ClickImage = new List<GameObject>();
    public List<Button> UIButton = new List<Button>();
    public Button[] EventButton;
    public GameObject ClickBlock;
    [HideInInspector] public TutorialController Controller;
    [HideInInspector] public TutorialRestaurantController RestaurantController;
    private int ActionNum;
    public void Start()
    {
        if (GameManager.Instance.gameMode.Equals(GameManager.GameMode.Tutorial))
        {
            Controller = GameObject.FindGameObjectWithTag("TutorialController").GetComponent<TutorialController>();
            RestaurantController = Controller.GetComponent<TutorialRestaurantController>();
            Init();
        }
    }
    private bool Use = false;
    private void OnEnable()
    {
        if (Use)
        {
            Init();
        }
        Use = true;
    }
    protected void Init()
    {
        ActionNum = 0;
        AddInit();
        foreach (var button in UIButton)
        {
            button.interactable = false;
        }
        foreach (var image in ClickImage)
        {
            image.SetActive(false);
        }
        for (int i = 0; i < EventButton.Length; i++) //클릭 이벤트 할당
        {
            if (EventButton[i] != null)
            {
                EventButton[i].onClick.AddListener(NextEvent);
            }
        }
        if (ClickBlock != null)
        {
            ClickBlock.SetActive(true);
        }
        ClickImage[0].SetActive(true);
        ClickAnimation(0);
    }
    protected virtual void AddInit(){ }
    protected virtual void AddEvent(int index) { }
    private void ClickAnimation(int index)
    {
        UIManager.UIScalePingPongAnimation(ClickImage[index]);
    }
    protected abstract void EndEvent();
    protected void NextEvent() //다음 클릭 이미지 활성화
    {
        if (ActionNum.Equals(ClickImage.Count - 1)) //exit버튼 누를경우 다음 대사
        {
            EndEvent();
            EventButton[ActionNum].onClick.RemoveListener(NextEvent);
            return;
        }
        if (EventButton[ActionNum] != null)
        {
            EventButton[ActionNum].interactable = false;
            EventButton[ActionNum].onClick.RemoveListener(NextEvent);
        }
        if (EventButton[ActionNum + 1] != null)
        {
            EventButton[ActionNum + 1].interactable = true;
        }
        ClickImage[ActionNum].SetActive(false); //이전 이미지, 버튼 비활성화
        ClickImage[ActionNum + 1].SetActive(true); //다음 이미지, 버튼 활성화
        ClickAnimation(ActionNum + 1);
        AddEvent(ActionNum);
        ActionNum++;
    }
}
