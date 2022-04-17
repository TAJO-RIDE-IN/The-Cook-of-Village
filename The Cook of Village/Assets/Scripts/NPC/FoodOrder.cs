/////////////////////////////////////
/// 학번 : 91914200
/// 이름 : JungNaEun 정나은
////////////////////////////////////
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

    private void Start()
    {
        camera = Camera.main;
        AddProbability();
        StartCoroutine(ChangeWithDelay.CheckDelay(FoodData.Instance.OrderTime, () => Order()));
    }
    private void Update()
    {
        //NPCUI.transform.rotation = Quaternion.LookRotation(transform.position - camera.transform.position);
    }
    private void AddProbability()
    {
        foreach(FoodTool i in FoodData.Instance.foodTool)
        {
            foreach (FoodInfos j in i.foodInfos)
            {
                FoodProbability.Add(j, j.OrderProbability);
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
        isOrder = true;
        NPCUI.SetActive(true);
        foodInfos = FoodProbability.Get();
        OrderFoodImage.sprite = foodInfos.ImageUI;
        var orderUI = ObjectPooling<OrderUI>.GetObject();
        orderUI.foodInfos = foodInfos;
        orderUI.gameObject.SetActive(true);
        StartCoroutine(WaitingOrder());
    }
}
