using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FoodOrder : MonoBehaviour
{
    Probability<FoodInfos> FoodProbability = new Probability<FoodInfos>();

    private void Start()
    {
        AddProbability();
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


}
