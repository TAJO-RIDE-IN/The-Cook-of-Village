using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TutorialCookingUI : TutorialDetailsUI
{
    public CookingTool BlenderTool;
    private int CookType;
    protected override void AddInit()
    {
        isCheck = false;
        EventButton[2] = null;
        EventButton[0] = RestaurantController.IngredientBox;
        StartCoroutine(ChangeWithDelay.CheckDelay(0.1f,() => Controller.PlayerControl(false, "Tool")));
    }
    protected override void AddEvent(int index)
    {
        switch(index)
        {
            case 0:
                Controller.PlayerControl(true, "Tool");
                EventButton[0].onClick.Invoke();
                EventButton[0].interactable = true;
                Controller.PlayerControl(false, "Tool");
                if(!RestaurantController.ToolInstall)
                {
                    Controller.NextDialogue();
                }
                ClickBlock.SetActive(false);
                break;
            case 1:
                ClickBlock.SetActive(true);
                ClickImage[2].SetActive(false);
                EventButton[1].interactable = true;
                EventButton[2] = EventButton[1];
                EventButton[2].onClick.AddListener(NextEvent);
                StartCoroutine(CheckCook());
                break;
        }
    }
    private IEnumerator CheckCook()
    {
        bool EndCook = false;
        while (!EndCook)
        {
            if (BlenderTool.isCooked)
            {
                ClickBlock.SetActive(false);
                ClickImage[2].SetActive(true);
                Controller.NextDialogue(); //시간 안에 꺼내지 않는다면 요리가 타버려요 출력
                StartCoroutine(CheckFailure());
                EndCook = true;
            }
            yield return null;
        }
    }
    bool isCheck = false;
    private IEnumerator CheckFailure()
    {
        while (!isCheck)
        {
            CookType = BlenderTool.FoodInfos.Type;
            yield return null;
        }
    }
    protected override void EndEvent()
    {
        isCheck = true;
        if (CookType.Equals(100001))
        {
            RestaurantController.CookingAgain();
        }
        else
        {
            Controller.EndEvent();
        }
        RestaurantController.PlayerCook.isSpace = false;
    }
}
