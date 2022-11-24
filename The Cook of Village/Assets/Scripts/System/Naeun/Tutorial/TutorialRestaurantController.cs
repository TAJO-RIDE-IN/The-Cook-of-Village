using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TutorialRestaurantController : MonoBehaviour
{
    [Header("RestaurantObject")]
    public GameObject CookPosition;
    public GameObject VillageParticle;
    public ThirdPersonMovement Player;

    [Header("NPCControl")]
    public NPCPooling npcPooling;
    public TutorialNPCController tutorialNPCController;

    public TutorialRestaurantUI tutorialRestaurantUI;
    private GameManager gameManager;
    private int ActionNum;
    private void Start()
    {
        gameManager = GameManager.Instance;
        if (gameManager.gameMode.Equals(GameManager.GameMode.Tutorial))
        {
            Init();
        }
    }
    private void Init()
    {
        //Player.StopMoving();
        npcPooling.enabled = false;
        gameManager.TutorialUI = true;     
        tutorialRestaurantUI.DialogueState(true);
        tutorialRestaurantUI.CallDialogue("Restaurant");
        VillageParticle.SetActive(false);
    }
    public void RestaurantAction()
    {
        switch (ActionNum)
        {
            case 0: //레스토랑 문열기
                gameManager.TutorialUI = false;
                tutorialNPCController.enabled = true;
                break;
            case 1: //손님 주문 대기
                tutorialNPCController.enabled = false;
                break;
        }
        ActionNum++;
    }

    public void NextDialogue()
    {
        tutorialRestaurantUI.DialogueText();
    }
}
