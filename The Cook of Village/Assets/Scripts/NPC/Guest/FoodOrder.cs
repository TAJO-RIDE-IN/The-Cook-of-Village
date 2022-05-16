/////////////////////////////////////
/// 학번 : 91914200
/// 이름 : JungNaEun 정나은
////////////////////////////////////
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class FoodOrder : MonoBehaviour, IObserver
{
    Probability<FoodInfos> FoodProbability = new Probability<FoodInfos>();
    [SerializeField]
    private FoodInfos foodInfos;
    public GameObject NPCUI;
    public Transform FoodPosition;
    public Image RemainingTimeImage;
    public Image OrderFoodImage;

    private bool CanReceive = false;
    private OrderUI currentOrderUI;
    private GuestNPC guest;
    private Transform camera;
    private void Awake()
    {
        camera = Camera.main.transform;
        guest = this.gameObject.GetComponent<GuestNPC>();
        guest.AddGuestNPC(new Guest());
        AddProbability();
    }
    private void Update()
    {
        NPCUI.transform.LookAt(NPCUI.transform.position + camera.rotation * Vector3.forward, camera.rotation * Vector3.up);
    }
    private void AddProbability() //확률 가중치 부여
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
            }
        }
    }
    private void EndOrder()
    {
        guest.ChangeState(GuestNPC.State.StandUP);
        StopCoroutine(WaitingOrder());
        NPCUI.SetActive(false);
        currentOrderUI.EndOrder(); //주문서
    }
    public void ReceiveFood(int ReceiveFood) //npc에게 음식 전달
    {
        if (ReceiveFood == foodInfos.ID && CanReceive) 
        {
            Instantiate(foodInfos.PrefabFood, FoodPosition);
            guest.ChangeState(GuestNPC.State.Eat);
            StartCoroutine(ChangeWithDelay.CheckDelay(FoodData.Instance.EatTime, () => guest.ChangeState(GuestNPC.State.StandUP)));
            EndOrder();
        }
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
    public void PayFood(int Price)
    {
        GameManager.Instance.Money += Price;
        guest.ChangeState(GuestNPC.State.Pay);
    }

    public void AddObserver() //MonoBehaviour 때문에 new 사용불가
    {
        guest.AddObserver(this);
    }

    public void Change(GuestNPC obj)
    {
        if (obj is GuestNPC)
        {
            var guestNPC = obj;
            if (guestNPC.CurrentState == GuestNPC.State.Sit)
            {
                StartCoroutine(ChangeWithDelay.CheckDelay(FoodData.Instance.OrderTime, () => Order()));
            }
        }
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
