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
    public GameObject NPCUI;
    public RestaurantNPC restaurantNPC;
    public Transform FoodPosition;
    public Image RemainingTimeImage;
    public Image OrderFoodImage;

    private bool CanReceive = false;
    private OrderUI currentOrderUI;
    private Transform camera;
    private void Start()
    {
        camera = Camera.main.transform;
        AddProbability();
        StartCoroutine(ChangeWithDelay.CheckDelay(FoodData.Instance.OrderTime, () => Order()));
    }
    private void Update()
    {
        NPCUI.transform.LookAt(NPCUI.transform.position + camera.rotation * Vector3.forward, camera.rotation * Vector3.up);
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
        while(time > 0)
        {
            time -= Time.deltaTime;
            float ratio = time / FoodData.Instance.WaitingTime;
            RemainingTimeImage.fillAmount = ratio;
            currentOrderUI.TimeBar.fillAmount = ratio;
            yield return null;
            if(time <= 0)
            {
                EndOrder();
                Debug.Log("TimeOver");
            }
        }
    }
    private void EndOrder()
    {
        StopCoroutine(WaitingOrder());
        NPCUI.SetActive(false);
        currentOrderUI.EndOrder();
        restaurantNPC.EatFood(foodInfos.Price);
    }
    public void ReceiveFood(int ReceiveFood)
    {
        if (ReceiveFood == foodInfos.ID && CanReceive)
        {
            Instantiate(foodInfos.PrefabFood, FoodPosition);
            EndOrder();
        }
    }
    public void ReciveButton() //테스트 버튼
    {
        ReceiveFood(foodInfos.ID);
    }
    private void Order()
    {
        NPCUI.SetActive(true);
        foodInfos = FoodProbability.Get();
        OrderFoodImage.sprite = foodInfos.ImageUI;
        currentOrderUI = ObjectPooling<OrderUI>.GetObject();
        currentOrderUI.foodInfos = foodInfos;
        currentOrderUI.gameObject.SetActive(true);
        StartCoroutine(WaitingOrder());
    }

    private void OnTriggerEnter(Collider other)
    {
        if(other.tag == "Player")
        {
            CanReceive = true;
        }
    }
    private void OnTriggerExit(Collider other)
    {
        if (other.tag == "Player")
        {
            CanReceive = false;
        }
    }
}
