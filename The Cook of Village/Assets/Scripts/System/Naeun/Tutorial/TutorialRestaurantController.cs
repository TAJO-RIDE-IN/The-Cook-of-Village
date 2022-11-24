using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TutorialRestaurantController : MonoBehaviour
{
    [Header("RestaurantObject")]
    public GameObject CookPosition;
    public GameObject VillageParticle;
    public GameObject MenuIcon;
    public ThirdPersonMovement Player;
    public NPCPooling npcPooling;

    [Header("Tutorial")]
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
        MenuIcon.SetActive(false);
        ChangeData();
    }
    private void ChangeData()
    {
        FoodData foodData = FoodData.Instance;
        foodData.EatTime = 3;
        foodData.foodTool[(int)FoodTool.Type.Plate].CanUse = false;
        foodData.foodTool[(int)FoodTool.Type.Blender].foodInfos[1].OrderProbability = 0;
        foodData.foodTool[(int)FoodTool.Type.Blender].foodInfos[2].OrderProbability = 0;
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
