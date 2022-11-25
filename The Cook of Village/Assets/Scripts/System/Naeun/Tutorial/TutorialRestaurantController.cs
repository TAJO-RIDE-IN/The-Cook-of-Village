using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TutorialRestaurantController : TutorialController
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
    public TutorialDestination FridgeDestination;

    private int ActionNum;
    public override void Init()
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
    /// <summary>
    /// Dialogue의 이름이 Restaurant인 경우 실행
    /// &(Action)이 있는 문장이 나올 경우 한 단계식 진행시킨다.
    /// </summary>
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
            case 2: //냉장고로 이동
                FridgeDestination.gameObject.SetActive(true);
                break;
            case 3: //냉장고 켜기
                //Player.StopMoving(); 
                break;
        }
        ActionNum++;
    }

    public override void NextDialogue()
    {
        tutorialRestaurantUI.DialogueText();
    }
}
