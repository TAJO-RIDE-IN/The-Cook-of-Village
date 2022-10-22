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
    public FoodInfos(int type, int id, string name, float makeTime, int price, float orderProbability, List<int> recipe, GameObject prefab, Sprite imageUI)
    {
        Type = type;
        ID = id;
        Name = name;
        MakeTime = makeTime;
        Price = price;
        OrderProbability = orderProbability;
        Recipe = recipe;
        PrefabFood = prefab;
        ImageUI = imageUI;
    }
}

[System.Serializable]
public class FoodTool
{
    public enum Type { Blender, Frypan, Pot, None, WhippingMachine, Oven, Failure}
    [SerializeField]
    public Type type;
    public Sprite ToolImage;
    public int Amount;
    public List<FoodInfos> foodInfos = new List<FoodInfos>();
    public FoodTool(Type _type,Sprite tool, List<FoodInfos> _foodInfos)
    {
        type = _type;
        ToolImage = tool;
        foodInfos = _foodInfos;
    }
}

public class FoodData : DataManager
{
    #region Singleton, LoadData
    private static FoodData instance = null;
    private void Awake() //씬 시작될때 인스턴스 초기화
    {
        if (null == instance)
        {
            instance = this;
        }
        else
        {
            Destroy(this.gameObject);
        }
    }
    public static FoodData Instance
    {
        get
        {
            if (null == instance)
            {
                return null;
            }
            return instance;
        }
    }
    #endregion
    public float OrderTime = 5f;
    public float DefaultWaitingTime = 30f;
    public float PayWaitingTime = 20f;
    public float ChaseUPTime = 20f;
    public float EatTime = 10f;
    public FoodTool[] foodTool;
    public FoodInfos RecipeFood(int type, List<int> recipe)
    {
        foreach(FoodInfos i in foodTool[type].foodInfos)
        {
            if(recipe.SequenceEqual(i.Recipe))
            {
                Debug.Log(i.Name);
                return i;
            }
        }
        return foodTool[(int)FoodTool.Type.Failure].foodInfos[0];
    }
    public override void SaveDataTime()
    {
        SaveArrayData<FoodTool>(ref foodTool, "FoodData");
    }

    public FoodInfos Foodinfos(int id)
    {
        int dataIndex;
        dataIndex = foodTool[FoodType(id)].foodInfos.FindIndex(m => m.ID == id);
        return foodTool[FoodType(id)].foodInfos[dataIndex];
    }
    public FoodTool FindFoodTool(int id)
    {
        int dataIndex;
        dataIndex = foodTool[FoodType(id)].foodInfos.FindIndex(m => m.ID == id);
        return foodTool[FoodType(id)];
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
