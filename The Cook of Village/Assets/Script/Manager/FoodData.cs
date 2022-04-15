/////////////////////////////////////
/// �й� : 91914200
/// �̸� : JungNaEun ������
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
    private void Awake() //�� ���۵ɶ� �ν��Ͻ� �ʱ�ȭ
    {
        if (null == instance)
        {
            instance = this;
            //LoadData<Food>(ref food, "FoodData"); //data �ϼ� �Ǿ����� �ٽ� Ȱ��ȭ
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
