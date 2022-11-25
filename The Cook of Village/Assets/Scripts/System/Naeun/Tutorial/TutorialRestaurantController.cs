using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TutorialRestaurantController : TutorialController
{
    [Header("RestaurantObject")]
    public GameObject CookPosition;
    public GameObject VillageParticle;
    public GameObject MenuIcon;
    public List<BoxCollider> ObjectCollider = new List<BoxCollider>();
    public ThirdPersonMovement Player;
    public NPCPooling npcPooling;

    [Header("Tutorial")]
    public TutorialNPCController tutorialNPCController;
    public TutorialRestaurantUI tutorialRestaurantUI;
    public TutorialDestination FridgeDestination;

    private int ActionNum;
    public override void Init()
    {
        foreach(var col in ObjectCollider)
        {
            if(col.isTrigger)
            {
                col.enabled = false;
            }
        }
        //Player.StopMoving();
        npcPooling.enabled = false;
        gameManager.TutorialUI = true;
        tutorialRestaurantUI.DialogueState(true);
        tutorialRestaurantUI.CallDialogue("Restaurant");
        VillageParticle.SetActive(false);
        CookPosition.SetActive(false);
        FridgeDestination.gameObject.SetActive(false);
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
    /// Dialogue�� �̸��� Restaurant�� ��� ����
    /// &(Action)�� �ִ� ������ ���� ��� �� �ܰ�� �����Ų��.
    /// </summary>
    public void RestaurantAction()
    {
        switch (ActionNum)
        {
            case 0: //������� ������
                gameManager.TutorialUI = false;
                tutorialNPCController.enabled = true;
                break;
            case 1: //�մ� �ֹ� ���
                tutorialNPCController.enabled = false;
                break;
            case 2: //������ �̵�
                FridgeDestination.gameObject.SetActive(true);
                ObjectCollider[3].enabled = true;
                break;
            case 3: //����� �ѱ�
                ObjectCollider[3].enabled = false;
                //Player.StopMoving(); 
                break;
        }
        ActionNum++;
    }
    public void CookingAction()
    {
        switch (ActionNum)
        {
            case 0: //������� �̵�

                break;
            case 1: //�ͼ��� ��ġ

                break;
            case 2: //�ͼ��⿡ ���� �ֱ�

                break;
        }
        ActionNum++;
    }

    public override void EndEvent()
    {
        tutorialRestaurantUI.CallDialogue("Cooking");
        ActionNum = 0;
    }
    public override void NextDialogue()
    {
        tutorialRestaurantUI.DialogueText();
    }
}
