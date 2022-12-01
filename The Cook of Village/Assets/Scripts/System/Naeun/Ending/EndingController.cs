using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EndingController : MonoBehaviour, IObserver<GameManager>
{
    [Header ("RestaurantObject")]
    public GameObject[] Object;
    public GameObject[] UI;

    [Header("EndingObject")]
    public EndingUI endingUI;
    public GameObject EndingDoorParticle;

    private int ActionNum;
    private bool PlayEnding;
    private GameManager gameManager;
    private GameData gameData;
    private DialogueManager dialogueManager;
    private SoundManager soundManager;
    private Dictionary<string, Action> DialogueAction = new Dictionary<string, Action>();
    private void Start()
    {
        gameManager = GameManager.Instance;
        gameData = GameData.Instance;
        dialogueManager = DialogueManager.Instance;
        soundManager = SoundManager.Instance;
        AddObserver(gameManager);
    }
    /// <summary>
    /// ���Ӹ�尡 Ending�� �� ��� ������ ��ħ�� ���� �ȴ�.
    /// </summary>
    public void EndingStart()
    {
        Init();
        PlayEnding = true;
    }
    private void Init()
    {
        foreach(var obj in Object)
        {
            obj.SetActive(false);
        }
        foreach (var ui in UI)
        {
            ui.SetActive(false);
        }
        AddAction();
        endingUI.endingController = this;
        endingUI.enabled = true;
        endingUI.CallDialogue("EndingStart");
    }
    private void AddAction()
    {
        DialogueAction.Add("EndingStart", () => StartAction());
        DialogueAction.Add("Ending", () => EndingAction());
    }
    /// <summary>
    /// ���忡�� &(Action)�� ���� ��� ����
    /// </summary>
    public void Action()
    {
        DialogueAction[dialogueManager.CurrentSentencesName]();
    }
    /// <summary>
    /// EndingStart ��ȭ�� ��� Action
    /// </summary>
    private void StartAction()
    {
        switch(ActionNum)
        {
            case 0:
                break;
            case 1:

                break;
            case 2:
                EndingDoorParticle.SetActive(true);
                endingUI.CanNext = false;
                break;
        }
        ActionNum++;
    }
    /// <summary>
    /// Ending ��ȭ�� ��� Action
    /// </summary>
    private void EndingAction() 
    {

    }
    
    public void NextEvent()
    {
        ActionNum = 0;
        endingUI.CanNext = true;
        endingUI.CallDialogue("Ending");
    }

    public void FinishEnding() //Ending������ ���
    {
        gameData.Ending = true;
        RemoveObserver(gameManager);
    }
    public void AddObserver(IGameManagerOb o)
    {
        o.AddObserver(this);
    }
    public void RemoveObserver(IGameManagerOb o)
    {
        o.RemoveObserver(this);
    }
    public void Change(GameManager obj)
    {
        if(obj is GameManager)
        {
            if(obj.gameMode.Equals(GameManager.GameMode.Ending) || !PlayEnding || !gameData.Ending)
            {
                EndingStart();
            }
        }
    }
}
