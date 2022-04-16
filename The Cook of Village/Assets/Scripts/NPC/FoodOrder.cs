using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class FoodOrder : MonoBehaviour
{
    Probability<FoodInfos> FoodProbability = new Probability<FoodInfos>();
    [SerializeField]
    private FoodInfos foodInfos;
    [SerializeField]
    private Camera camera;
    public GameObject NPCUI;
    public Image RemainingTimeImage;
    public Image OrderFoodImage;

    private bool isOrder = false;
    private bool IsOrder
    {
        get { return isOrder; }
        set
        {
            isOrder = value;
            Order();
        }
    }

    private void Start()
    {
        camera = Camera.main;
        AddProbability();
        StartCoroutine(IsOrder.CheckDelay<bool>(true, FoodData.Instance.OrderTime, value => IsOrder = value));
    }
    private void Update()
    {
        //NPCUI.transform.rotation = Quaternion.LookRotation(transform.position - camera.transform.position);
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
    private IEnumerator WaitingOrder()
    {
        float time = FoodData.Instance.WaitingTime;
        while(time > 0 && isOrder)
        {
            time -= Time.deltaTime;
            RemainingTimeImage.fillAmount = time / FoodData.Instance.WaitingTime;
            yield return null;
            if(time <= 0)
            {
                EndOrder();
            }
        }
    }
    private void EndOrder()
    {
        NPCUI.SetActive(false);
    }
    private void Order()
    {
        NPCUI.SetActive(true);
        foodInfos = FoodProbability.Get();
        OrderFoodImage.sprite = foodInfos.ImageUI;
        StartCoroutine(WaitingOrder());
    }
}
