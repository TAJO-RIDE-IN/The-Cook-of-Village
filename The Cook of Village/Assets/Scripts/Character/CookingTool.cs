using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class CookingTool : MonoBehaviour
{
    public enum Type { Blender = 0, Pot = 1, Frypan = 2}
    public Type type;
    
    public GameObject Inven;
    
    public List<int> ingredientList = new List<int>();
    public MaterialInfos currentMaterialInTool; //이걸로 리스트를 아예 만들어서 3개Infos를 다 저장해놓을지 ID만 3개 저장해놓을지 추후 수정

    public FoodInfos foodInfos;

    /*[RuntimeInitializeOnLoadMethod(RuntimeInitializeLoadType.BeforeSceneLoad)]
    static void FirstLoad(){
        
    }*/
    private void Start()
    {
        foodInfos = null;//음식을 넣었다가 레스토랑 밖으로 나갔다올때 초기화되는건 아니겠지
    }

    public void PutIngredient() //이걸 현재 들고있는게 null이 아닐때만 실행시켜주면 되는데 혹시몰라서 한번 더 조건문 넣음
    {

        for (int i = 0; i < 3; i++) //일단 레시피에 들어가는 최대 재료 개수가 3개라고 했을 때
        {
            if (ingredientList.Count == i)
            {
                if (currentMaterialInTool != null)
                {
                    ingredientList.Add(currentMaterialInTool.ID);
                    Inven.SetActive(true);
                    Inven.transform.GetChild(i).transform.GetComponent<Image>().sprite = currentMaterialInTool.ImageUI;
                }
                break;
            }
        }
    }

    public void Cook()
    {
        foodInfos = FoodData.Instance.RecipeFood((int)type, ingredientList);
        
        Debug.Log(foodInfos.Name);
    }

}
