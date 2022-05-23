using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class CookingTool : MonoBehaviour
{
    public enum Type { Blender = 0, Pot = 1, FryPan = 2}//접시도 추가할거니까 접시일때 행동들이랑 도구일때 행동들 구분하기, 그리고 머랭같은 특별한 도구도 어떻게할지 생각해야함
    public Type type;

    private GameObject IngredientInven;
    private GameObject FoodInven;
    private Image blackCircle;
    private Animation animation;
    private Coroutine coroutine;
    
    private float currentValue;
    
    public List<int> ingredientList = new List<int>();
    public FoodInfos FoodInfos { get; set;}//foodInfos가 바뀌면 해줄 일,즉 UI코루틴 끝났을때 할 일 set에 적자
    
    public bool isBeforeCooking;
    public bool isCooked;
    

    /*[RuntimeInitializeOnLoadMethod(RuntimeInitializeLoadType.BeforeSceneLoad)]
    static void FirstLoad(){
        
    }*/
    private void Start()
    {
        animation = transform.GetComponent<Animation>();
        IngredientInven = transform.GetChild(1).GetChild(0).gameObject;
        FoodInven = transform.GetChild(1).GetChild(1).gameObject;
        blackCircle = FoodInven.transform.GetChild(1).GetComponent<Image>();
    }


    public bool PutIngredient(int id, Sprite sprite) //이걸 현재 들고있는게 null이 아닐때만 실행시켜주면 되는데 혹시몰라서 한번 더 조건문 넣음
    {
        for (int i = 0; i < 3; i++) //일단 레시피에 들어가는 최대 재료 개수가 3개라고 했을 때
        {
            if (ingredientList.Count == i)
            {
                animation.Play(type.ToString());
                //Debug.Log("애니메이션실행!");
                ingredientList.Add(id);
                IngredientInven.SetActive(true);
                IngredientInven.transform.GetChild(i).transform.GetComponent<Image>().sprite = sprite;
                return true;
            }
            else
            {
                //도구에 재료 꽉찼는데 넣으려고할때 행동
            }
            
        } return false;
    }

    public void Cook()
    {
        ingredientList.Sort();
        FoodInfos = FoodData.Instance.RecipeFood((int)type, ingredientList);
        //Debug.Log(FoodInfos.Name);
        IngredientInven.SetActive(false);
        coroutine = StartCoroutine(CookingGauge());
    }

    public void RefreshTool()
    {
        ingredientList.Clear();
        for (int i = 0; i < IngredientInven.transform.childCount; i++)
        {
            IngredientInven.transform.GetChild(i).GetComponent<Image>().sprite = Resources.Load<Sprite>("IngriedientBox");
        }
        FoodInven.transform.GetChild(2).gameObject.SetActive(false);
        FoodInven.transform.GetChild(3).gameObject.SetActive(true);
        blackCircle.fillAmount = 0;
        isBeforeCooking = true;
        isCooked = false;

    }
    
    IEnumerator CookingGauge() //LoadingBar.fillAmount이 1이 될때까지 점점 게이지를 추가해줌
    {
        isBeforeCooking = false;
        while (blackCircle.fillAmount < 1)
        {
            currentValue += Time.deltaTime;
            blackCircle.fillAmount = currentValue / FoodInfos.MakeTime;
            yield return null;
            /*if (!_wallCollision.iscollide)
            {
                StopCoroutine(co_my_coroutine); //요리 멈출때
            }*/
        }

        
        isCooked = true;
        FoodInven.transform.GetChild(2).transform.GetComponent<Image>().sprite = FoodInfos.ImageUI;
        FoodInven.transform.GetChild(3).gameObject.SetActive(false);//디폴트 숨기기
        FoodInven.transform.GetChild(2).gameObject.SetActive(true);
    }


}
