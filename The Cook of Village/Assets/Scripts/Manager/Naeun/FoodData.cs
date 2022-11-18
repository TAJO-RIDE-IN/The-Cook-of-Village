/////////////////////////////////////
/// 학번 : 91914200
/// 이름 : JungNaEun 정나은
////////////////////////////////////
using System.Collections;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using UnityEngine;
using UnityEngine.UI;

[System.Serializable]
public class FoodInfos
{
    public int Type;
    public int ID;
    public string Name;
    public string KoreanName;
    [Multiline]
    public string Explanation;
    public float MakeTime;
    public float BurntTime;
    public int Price;
    public float OrderProbability;
    public List<int> Recipe = new List<int>();
    public GameObject PrefabFood;
    public Sprite ImageUI; //UIImage
}

[System.Serializable]
public class FoodTool
{
    public enum Type { Blender, FryPan, Pot, Plate, Whipper, Oven, Failure}
    [SerializeField]
    public Type type;
    public Sprite ToolImage;
    public bool CanUse;
    public List<FoodInfos> foodInfos = new List<FoodInfos>();
}

public class FoodData : DataManager<FoodData>
{
    public float OrderTime = 5f;
    public float DefaultWaitingTime = 30f;
    public float PayWaitingTime = 20f;
    public float ChaseUPTime = 20f;
    public float EatTime = 10f;
    public FoodTool[] foodTool;
    /// <summary>
    /// 레시피가 맞는 요리가 있는지 확인 후 요리정보 리턴
    /// </summary>
    /// <param name="type"> 요리도구 선택 Blender = 0 , Frypa = 1, Pot = 2, Plate = 3, WhippingMachine = 4, Oven = 5, Failure = 6</param>
    /// <param name="recipe"> 요리도구에 넣은 재료 리스트 </param>
    /// <returns>레시피에 맞는 FoodInfos 리턴</returns>
    public FoodInfos RecipeFood(int type, List<int> recipe)
    {
        foreach(FoodInfos infos in foodTool[type].foodInfos)
        {
            if(recipe.SequenceEqual(infos.Recipe))
            {
                Debug.Log(infos.Name);
                return infos;
            }
        }
        return foodTool[(int)FoodTool.Type.Failure].foodInfos[0];
    }
    public override void SaveDataTime(string PlayName)
    {
        SaveArrayData<FoodTool>(ref foodTool, "FoodData" + PlayName, PlayName);
    }
    /// <summary>
    /// Food ID에 따른 Food 정보 찾기
    /// </summary>
    /// <param name="id">Food ID를 입력</param>
    /// <returns>FoodInfos 리턴</returns>
    public FoodInfos Foodinfos(int id)
    {
        int dataIndex;
        dataIndex = foodTool[FoodType(id)].foodInfos.FindIndex(m => m.ID == id);
        return foodTool[FoodType(id)].foodInfos[dataIndex];
    }
    /// <summary>
    /// Food ID에 따른 도구 찾기
    /// </summary>
    /// <param name="id">Food ID를 입력</param>
    /// <returns>Food Tool을 리턴함</returns>
    public FoodTool FindFoodTool(int FoodId)
    {
        return foodTool[FoodType(FoodId)];
    }
    /// <summary>
    /// ItemId를 입력하여 FoodTool를 찾기
    /// </summary>
    /// <param name="ItemID">ItemInfos에 있는 ID입력</param>
    /// <returns>FoodTool을 리턴함.</returns>
    public FoodTool ItemIdToFoodTool(int ItemID)
    {
        int id = ItemID % 10; //60번 부터 시작하기 때문에 1의 자리만 남김.
        return foodTool[id];
    }
    public void CanUseTool(int ItemID)
    {
        ItemIdToFoodTool(ItemID).CanUse = true;
    }
    public bool DrinkFood(int type)
    {
        return type == 0;
    }

    private int FoodType(int id)
    {
        return id / 10;
    }
}
