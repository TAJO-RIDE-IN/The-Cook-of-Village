using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EndingController : MonoBehaviour, IObserver<GameManager>
{
    [Header("RestaurantObject")]
    public ThirdPersonMovement Player;
    public GameObject[] Object;
    public GameObject[] UI;

    [Header("EndingObject")]
    public EndingUI endingUI;
    public EndingFather Father;
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
    /// 게임모드가 Ending이 될 경우 다음날 아침에 실행 된다.
    /// </summary>
    public void EndingStart()
    {
        Init();
        PlayEnding = true;
    }
    private void Init()
    {
        RestaurantObjectState(false);
        AddAction();
        Father.gameObject.SetActive(true);
        endingUI.endingController = this;
        endingUI.enabled = true;
        endingUI.CallDialogue("EndingStart");
    }
    private void RestaurantObjectState(bool state)
    {
        foreach (var obj in Object)
        {
            obj.SetActive(state);
        }
        foreach (var ui in UI)
        {
            ui.SetActive(state);
        }
    }
    private void AddAction()
    {
        DialogueAction.Add("EndingStart", () => StartAction());
        DialogueAction.Add("Ending", () => EndingAction());
    }
    /// <summary>
    /// 문장에서 &(Action)이 있을 경우 실행
    /// </summary>
    public void Action()
    {
        DialogueAction[dialogueManager.CurrentSentencesName]();
    }
    /// <summary>
    /// EndingStart 대화인 경우 Action
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
    /// Ending 대화인 경우 Action
    /// </summary>
    private void EndingAction() 
    {
        Player.isLocked = true;
        
        switch (ActionNum)
        {
            case 0:
                break;
            case 1:
                break;
            case 2:
                break;
            case 3:
                break;
            case 4:
                break;
            case 5:
                break;
            case 6:
                break;
            case 7:
                break;
            case 8:
                break;
            case 9:
                break;
            case 10:
                break;
            case 11:
                Father.currentState = EndingFather.FatherState.Nod;
                break;
            case 12:
                FatherAppearance(1);
                endingUI.CanNext = true;
                break;
            case 13:
                FinishEnding();
                break;

        }
        ActionNum++;
    }
    
    public void FatherAppearance(int index)
    {
        if(index.Equals(0))
        {
            Player.StopWalking();
            Player.gameObject.transform.LookAt(Object[0].transform); //문 쳐다보기
        }
        StartCoroutine(Father.FatherAppearance(index));
    }

    public void NextEvent()
    {
        ActionNum = 0;
        endingUI.CanNext = true;
        endingUI.CallDialogue("Ending");
    }
    public void NextDialogue()
    {
        endingUI.CanNext = true;
        endingUI.DialogueText();
    }
    public void FinishEnding() //Ending끝났을 경우
    {
        Player.StartWalking();
        gameData.Ending = true;
        gameManager.gameMode = GameManager.GameMode.Default;
        RestaurantObjectState(true);
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
