using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public abstract class TutorialDetailsUI : MonoBehaviour
{
    public List<GameObject> ClickImage = new List<GameObject>();
    public List<Button> UIButton = new List<Button>();
    public Button[] EventButton;
    public GameObject ClickBlock;
    public TutorialController Controller;

    public void Start()
    {
        if (GameManager.Instance.gameMode.Equals(GameManager.GameMode.Tutorial))
        {
            Controller.NextDialogue();
            Init();
        }
    }
    private void Init()
    {
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
            int index = i;
            if (EventButton[i] != null)
            {
                EventButton[i].onClick.AddListener(() => NextEvent(index));
            }
        }
        ClickBlock.SetActive(true);
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
    protected void NextEvent(int index) //���� Ŭ�� �̹��� Ȱ��ȭ
    {
        if (index.Equals(ClickImage.Count - 1)) //exit��ư ������� ���� ���
        {
            EndEvent();
            return;
        }
        ClickImage[index].SetActive(false); //���� �̹���, ��ư ��Ȱ��ȭ
        ClickImage[index + 1].SetActive(true); //���� �̹���, ��ư Ȱ��ȭ
        ClickAnimation(index + 1);
        if (EventButton[index] != null)
        {
            EventButton[index].interactable = false;
        }
        if (EventButton[index + 1] != null)
        {
            EventButton[index + 1].interactable = true;
        }
        AddEvent(index);
    }
}
