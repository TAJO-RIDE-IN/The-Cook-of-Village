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
    public TutorialController Controller;
    private int ActionNum;
    public void Start()
    {
        if (GameManager.Instance.gameMode.Equals(GameManager.GameMode.Tutorial))
        {
            Controller = GameObject.FindGameObjectWithTag("TutorialController").GetComponent<TutorialController>();
            Controller.NextDialogue();
            Init();
        }
    }
    private void Init()
    {
        ActionNum = 0;
        foreach (var button in UIButton)
        {
            button.interactable = false;
        }
        foreach (var image in ClickImage)
        {
            image.SetActive(false);
        }
        for (int i = 0; i < EventButton.Length; i++) //Ŭ�� �̺�Ʈ �Ҵ�
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
        AddInit();
    }
    protected virtual void AddInit(){ }
    protected virtual void AddEvent(int index) { }
    private void ClickAnimation(int index)
    {
        UIManager.UIScalePingPongAnimation(ClickImage[index], new Vector3(1.3f, 1.3f, 1.3f), 0.5f);
    }
    protected abstract void EndEvent();
    protected void NextEvent() //���� Ŭ�� �̹��� Ȱ��ȭ
    {
        if (ActionNum.Equals(ClickImage.Count - 1)) //exit��ư ������� ���� ���
        {
            EndEvent();
            return;
        }
        ClickImage[ActionNum].SetActive(false); //���� �̹���, ��ư ��Ȱ��ȭ
        ClickImage[ActionNum + 1].SetActive(true); //���� �̹���, ��ư Ȱ��ȭ
        if (EventButton[ActionNum] != null)
        {
            EventButton[ActionNum].interactable = false;
            EventButton[ActionNum].onClick.RemoveListener(NextEvent);
        }
        if (EventButton[ActionNum + 1] != null)
        {
            EventButton[ActionNum + 1].interactable = true;
        }
        ClickAnimation(ActionNum + 1);
        AddEvent(ActionNum);
        ActionNum++;
    }
}
