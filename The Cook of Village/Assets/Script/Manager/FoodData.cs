/////////////////////////////////////
/// 학번 : 91914200
/// 이름 : JungNaEun 정나은
////////////////////////////////////
using System.Collections;
using System.Collections.Generic;
using System.IO;
using UnityEngine;
using UnityEngine.UI;

[System.Serializable]
public class FoodInfos
{
    public int ID;
    public string Name;
    public float MakeTime;
    public int Price;
    public int OrderProbability;
    public List<int> Recipe = new List<int>();
    public GameObject PrefabFood;
    public Image ImageUI; //UIImage
    public FoodInfos(int id, string name, float makeTime, int price, int orderProbability, List<int> recipe, GameObject prefab, Image imageUI)
    {
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
public class Food
{
    public enum Type { Blender, Pot, Frypan }
    [SerializeField]
    public Type type;
    public List<FoodInfos> foodInfos = new List<FoodInfos>();
    public Food(Type _type, List<FoodInfos> _foodInfos)
    {
        type = _type;
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
            //LoadData<Food>(ref food, "FoodData"); //data 완성 되었을때 다시 활성화
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
    [SerializeField]
    public Food[] food;
    [ContextMenu("To Json Data")]

    public override void SaveDataTime()
    {
        SaveData<Food>(ref food, "FoodData");
    }
}
