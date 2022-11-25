using System;
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
    public CookingCharacter PlayerCook;
    public NPCPooling npcPooling;

    [Header("Tutorial")]
    public TutorialNPCController tutorialNPCController;
    public TutorialRestaurantUI tutorialRestaurantUI;
    public TutorialDestination FridgeDestination;
    private int ActionNum;
    private Dictionary<string, Action> CurrentAction = new Dictionary<string, Action>();
    private Dictionary<string, string> NextDialogueName = new Dictionary<string, string>()
    {
        {"Control", "Purchase"}, {"Purchase", "Restaurant"}, {"Restaurant", "Fridge"},
        {"Fridge", "Cooking"}, {"Cooking", "Serving"}
    };
    public override void Init()
    {
        AddActionDictionary();
        foreach (var col in ObjectCollider)
        {
            if(col.isTrigger)
            {
                col.enabled = false;
            }
        }
        //Player.StopMoving();
        npcPooling.enabled = false;
        gameManager.TutorialUI = true;
        VillageParticle.SetActive(false);
        CookPosition.SetActive(false);
        FridgeDestination.gameObject.SetActive(false);
        MenuIcon.SetActive(false);
        ChangeData();
        tutorialRestaurantUI.DialogueState(true);
        tutorialRestaurantUI.CallDialogue("Restaurant");
    }
    private void AddActionDictionary()
    {
        CurrentAction.Add("Restaurant", () => RestaurantAction());
        CurrentAction.Add("Fridge", () => FridgeAction());
        CurrentAction.Add("Cooking", () => CookingAction());
        CurrentAction.Add("CharredFood", () => RestaurantAction());
        CurrentAction.Add("Serving", () => RestaurantAction());
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
        }
        ActionNum++;
    }
    public void FridgeAction()
    {
        switch (ActionNum)
        {
            case 0: //������ �̵�
                FridgeDestination.gameObject.SetActive(true);
                ObjectCollider[3].enabled = true;
                break;
            case 1: //����� ����
                FridgeDestination.gameObject.SetActive(false);
                ObjectCollider[3].enabled = false;
                break;
            case 3: //����� ��� ������
                PlayerCook.isFridgeCollider = false;
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
    public void PlayAction()
    {
        CurrentAction[dialogueManager.CurrentSentencesName]();
    }
    public override void EndEvent()
    {
        ActionNum = 0;
        tutorialRestaurantUI.CallDialogue(NextDialogueName[dialogueManager.CurrentSentencesName]);
    }
    public override void NextDialogue()
    {
        tutorialRestaurantUI.DialogueText();
    }
}
