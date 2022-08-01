using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

using UnityEngine.UI;
using Object = System.Object;

public class CookingTool : MonoBehaviour
{
    public enum Type { Blender = 0, FryPan = 1, Pot = 2, Oven = 3, Whisker = 4, Trash = 5}//접시도 추가할거니까 접시일때 행동들이랑 도구일때 행동들 구분하기, 그리고 머랭같은 특별한 도구도 어떻게할지 생각해야함
    public Type type;

    public GameObject InventoryBig;
    public GameObject IngredientInven;
    
    public Image[] Ing = new Image[3];
    public Image food;
    public Image foodBig;
    public Image blackCircle;

    public GameObject circleUI;
    public GameObject circleUIBig;
    
    public Sprite toolBeforeCook;
    public CookItemSlotManager cookSlotManager;


    private Animation _animation;
    private float currentValue;
    
    
    public List<int> ingredientList = new List<int>();
    public FoodInfos FoodInfos { get; set;}//foodInfos가 바뀌면 해줄 일,즉 UI코루틴 끝났을때 할 일 set에 적자
    
    [HideInInspector]public bool isBeforeCooking = true;
    [HideInInspector]public bool isCooked;
    

    /*[RuntimeInitializeOnLoadMethod(RuntimeInitializeLoadType.BeforeSceneLoad)]
    static void FirstLoad(){
        
    }*/
    
    private void Start()
    {
        //InventoryBig.SetActive(false);
        _animation = transform.GetComponent<Animation>();
    }

    
    public void ReturnIngredient(int i)
    {
        if (ingredientList.Count > 0)
        {
            if (InventoryManager.Instance.AddIngredient(ItemData.Instance.IngredientsInfos(ingredientList[i])))
            {
                cookSlotManager.itemslots[i].changeSlotUI(cookSlotManager.emptySlot);
            }
            
        }
    }

    public bool PutIngredient(int id, Sprite sprite) //이걸 현재 들고있는게 null이 아닐때만 실행시켜주면 되는데 혹시몰라서 한번 더 조건문 넣음
    {
        for (int i = 0; i < cookSlotManager.ChildSlotCount; i++) //일단 레시피에 들어가는 최대 재료 개수가 3개라고 했을 때
        {
            if (ingredientList.Count == i)
            {
                _animation.Play(
                    type.ToString());
                //Debug.Log("애니메이션실행!");
                ingredientList.Add(id);
                cookSlotManager.itemslots[i].changeSlotUI(sprite);
                if (type != Type.Trash)
                {
                    Ing[i].sprite = sprite;
                    IngredientInven.SetActive(true);
                }
                return true;
            }
            
        }
        //도구에 재료 꽉찼는데 넣으려고할때 행동
        return false;
    }

    public void Cook()
    {
        if (ingredientList.Count > 0)
        {
            isBeforeCooking = false;
            ingredientList.Sort();
            FoodInfos = FoodData.Instance.RecipeFood((int)type, ingredientList);
            cookSlotManager.RefreshSlot();
            RemoveIng();
            ingredientList.Clear();
            //Debug.Log(FoodInfos.Name);
            IngredientInven.SetActive(false);
            StartCoroutine(CookingGauge());
        }
        else
        {
            //재료를 넣으세요! UI 출력
        }
        
    }

    public void RemoveFood()
    {
        blackCircle.fillAmount = 0;
        isCooked = false;
        food.sprite = toolBeforeCook;
    }

    public void RemoveIng()
    {
        for (int i = 0; i < 3; i++)
        {
            Ing[i].sprite = cookSlotManager.emptySlot;
        }
        isBeforeCooking = true;

    }

    public void CloseUI()
    {
        IngredientInven.SetActive(false);
    }

    
    IEnumerator CookingGauge() //LoadingBar.fillAmount이 1이 될때까지 점점 게이지를 추가해줌
    {
        while (blackCircle.fillAmount < 1)
        {
            currentValue += Time.deltaTime;
            blackCircle.fillAmount = currentValue / FoodInfos.MakeTime;
            circleUI.transform.Rotate(0, 0, 1);
            circleUIBig.transform.Rotate(0, 0, 1);
            yield return null;
        }
        currentValue = 0;
        isCooked = true;
        food.sprite = FoodInfos.ImageUI;
        foodBig.sprite = FoodInfos.ImageUI;
    }


}
