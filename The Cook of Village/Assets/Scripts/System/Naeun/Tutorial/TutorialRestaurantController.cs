using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class TutorialRestaurantController : TutorialController
{
    [Header("RestaurantObject")]
    public List<BoxCollider> ObjectCollider = new List<BoxCollider>();
    public GameObject[] CookPosition;
    public GameObject VillageParticle;
    public GameObject MenuIcon;
    public ThirdPersonMovement Player;
    public CookingCharacter PlayerCook;
    public NPCPooling npcPooling;
    public Button IngredientBox;

    [Header("Tutorial")]
    public GameObject[] RestaurantDestination;
    public TutorialDetailsUI[] tutorialDetailsUI;
    public TutorialNPCController tutorialNPCController;
    public TutorialGuestOrder tutorialGuestOrder;
    public TutorialRestaurantUI tutorialRestaurantUI;
    public ChangeScene changeScene;

    private int ActionNum;
    [HideInInspector]public bool ToolInstall;
    private Dictionary<string, Action> CurrentAction = new Dictionary<string, Action>();
    private Dictionary<string, string> NextDialogueName = new Dictionary<string, string>()
    {
        {"Restaurant", "Fridge"},{"Fridge", "InstallTool"}, {"InstallTool", "Cooking"}, 
        {"Cooking", "Serving"}, {"CharredFood", "Fridge"}, {"Serving", "EndTutorial"}
    };
    private Dictionary<string, string> NextAgainDialogueName = new Dictionary<string, string>()
    {
        {"Restaurant", "Fridge"},{"Fridge", "Cooking"}, {"Cooking", "Serving"}, {"CharredFood", "Fridge"}, {"Serving", "EndTutorial"}
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
        foreach(var details in tutorialDetailsUI)
        {
            details.enabled = true;
        }
        foreach(var destination in RestaurantDestination)
        {
            destination.SetActive(false);
        }
        foreach(var cook in CookPosition)
        {
            cook.SetActive(false);
        }
        Player.StopWalking();
        ToolInstall = false;
        npcPooling.enabled = false;
        gameManager.TutorialUI = true;
        VillageParticle.SetActive(false);
        MenuIcon.SetActive(false);
        ChangeData();
        tutorialRestaurantUI.enabled = true;
        tutorialRestaurantUI.DialogueState(true);
        tutorialRestaurantUI.CallDialogue("Restaurant");
    }
    private void AddActionDictionary()
    {
        CurrentAction.Add("Restaurant", () => RestaurantAction());
        CurrentAction.Add("Fridge", () => FridgeAction());
        CurrentAction.Add("InstallTool", () => InstallToolAction());
        CurrentAction.Add("Cooking", () => CookingAction());
        CurrentAction.Add("CharredFood", () => CharredFoodAcation());
        CurrentAction.Add("Serving", () => ServingAcation());
    }
    private void ChangeData()
    {
        FoodData foodData = FoodData.Instance;
        foodData.EatTime = 3;
        foodData.foodTool[(int)FoodTool.Type.Plate].CanUse = false;
        foodData.foodTool[(int)FoodTool.Type.Blender].foodInfos[1].OrderProbability = 0;
        foodData.foodTool[(int)FoodTool.Type.Blender].foodInfos[2].OrderProbability = 0;
    }
    #region Action
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
                Player.StartWalking();
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
                Player.StartWalking();
                RestaurantDestination[0].SetActive(true);
                break;
            case 1: //����� ����
                Player.StopWalking();
                RestaurantDestination[0].gameObject.SetActive(false);
                ObjectCollider[3].enabled = true;
                break;
            case 3: //����� ��� ������
                break;
        }
        ActionNum++;
    }
    public void InstallToolAction()
    {
        switch (ActionNum)
        {
            case 0: //������� �̵�
                Player.StartWalking();
                CookPosition[0].SetActive(true);
                RestaurantDestination[1].SetActive(true);
                break;
            case 1: //������ ����, �ͼ��� ��ġ
                Player.StopWalking();
                break;
        }
        ActionNum++;
    }
    public void CookingAction()
    {
        //���
        if(ToolInstall)
        {
            switch (ActionNum)
            {
                case 0: //������� �̵� 
                    Player.StartWalking();
                    RestaurantDestination[1].SetActive(true);
                    break;
                case 1: //������ ���� �� ���� �ֱ�
                    Player.StopWalking();
                    break;
            }
        }
        ActionNum++;
    }
    public void ServingAcation()
    {
        switch (ActionNum)
        {
            case 0: //�մԿ��� �̵�
                Player.StartWalking();
                RestaurantDestination[3].SetActive(true);
                break;
            case 1: //�մ� ���� ����
                Player.StopWalking();
                PlayerCook._foodOrder = tutorialGuestOrder;
                PlayerCook.isGuestCollider = true;
                StartCoroutine(CheckSpace());
                break;
            case 2: //����� �̵�
                Player.StartWalking();
                RestaurantDestination[4].SetActive(true);
                break;
            case 3: //���� ����
                Player.StopWalking();
                break;
        }
        ActionNum++;
    }
    private IEnumerator CheckSpace()
    {
        bool check = false;
        while(!check)
        {
            if(Input.GetKeyDown(KeyCode.Space))
            {
                tutorialRestaurantUI.ClickImage.gameObject.SetActive(true);
                UIManager.UIScalePingPongAnimation(tutorialRestaurantUI.ClickImage.gameObject);
                IngredientBox.onClick.AddListener(ServingButton); //�ֹ����� ������ �������� �����̱� ������ ���� �Ҵ�����
                tutorialRestaurantUI.gameObject.SetActive(false);
                check = true;
            }
            yield return null;
        }
    }
    public void ToolEnable()
    {
        Transform tool = PlayerCook._cookingTool.InventoryBig.transform;
        tool.Find("TutorialCooking").GetComponent<TutorialCookingUI>().enabled = true;
    }
    private void ServingButton() //�丮 ���� ��ư�̺�Ʈ
    {
        Player.StartWalking();
        tutorialRestaurantUI.ClickImage.gameObject.SetActive(false);
        IngredientBox.onClick.RemoveListener(ServingButton);
    }
    public void CharredFoodAcation()
    {
        ToolInstall = true;
        switch (ActionNum)
        {
            case 0: //������������ �̵�
                Player.StartWalking();
                RestaurantDestination[2].SetActive(true);
                break;
            case 1://�������� ����
                Player.StopWalking();
                StartCoroutine(ChangeWithDelay.CheckDelay(0.01f, () => { PlayerCook.objectName = "Trash"; })); //Cooking��ũ��Ʈ�� �ν��� �ʰ� �Ǳ� ������
                break;
        }
        ActionNum++;
    }
    #endregion
    public override void PlayerControl(bool state, string name)
    {
        if(state)
        {
            PlayerCook.isSpace = true;
        }
        switch (name)
        {
            case "Fridge":
                PlayerCook.isFridgeCollider = state;
                break;
            case "Tool":
                PlayerCook.isToolCollider = state;
                break;
            case "CookPosition":
                PlayerCook.isCookPositionCollider = state;
                break;
        }
    }
    public override void PlayAction(bool state)
    {
        if(state)
        {
            CurrentAction[dialogueManager.CurrentSentencesName]();
        }
        else
        {
            Player.StopWalking();
        }
    }
    public override void EndEvent()
    {
        ActionNum = 0;
        string dialogueName = (ToolInstall) ? NextAgainDialogueName[dialogueManager.CurrentSentencesName] : NextDialogueName[dialogueManager.CurrentSentencesName];
        tutorialRestaurantUI.CallDialogue(dialogueName);
    }
    public void EndTutorial()
    {
        changeScene.MoveScene();
    }
    /// <summary>
    /// ������ ���� ��� �������뿡 ������ ����
    /// </summary>
    public void CookingAgain()
    {
        ActionNum = 0;
        tutorialRestaurantUI.CallDialogue("CharredFood");
    }
    public override void NextDialogue()
    {
        tutorialRestaurantUI.DialogueText();
    }
}
