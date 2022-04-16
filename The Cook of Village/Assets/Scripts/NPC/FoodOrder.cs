using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FoodOrder : MonoBehaviour
{
    Probability<FoodInfos> FoodProbability = new Probability<FoodInfos>();
    public FoodInfos foodInfos;

    private void Start()
    {
        AddProbability();
        Invoke("Order", 5.0f);
    }

    private void AddProbability()
    {
        for (int i = 0; i < FoodData.Instance.foodTool.Length; i++)
        {
            for (int j = 0; j < FoodData.Instance.foodTool[i].foodInfos.Count; j++)
            {
                FoodProbability.Add(FoodData.Instance.foodTool[i].foodInfos[j], FoodData.Instance.foodTool[i].foodInfos[j].OrderProbability);
            }
        }
    }

    private void Order()
    {
        foodInfos = FoodProbability.Get();
    }
}
