/////////////////////////////////////
/// �й� : 91914200
/// �̸� : JungNaEun ������
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
    public float MakeTime;
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
    public enum Type { Blender, Pot, Frypan, Failure}
    [SerializeField]
    public Type type;
    public Sprite ToolImage;
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
    private void Awake() //�� ���۵ɶ� �ν��Ͻ� �ʱ�ȭ
    {
        if (null == instance)
        {
            instance = this;
            //LoadData<FoodTool>(ref food, "FoodData"); //data �ϼ� �Ǿ����� �ٽ� Ȱ��ȭ
            DontDestroyOnLoad(this.gameObject);
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
    public float FoodWaitingTime = 20f;
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
        return foodTool[3].foodInfos[0];
    }
    public override void SaveDataTime()
    {
        SaveArrayData<FoodTool>(ref foodTool, "FoodData");
    }
}
