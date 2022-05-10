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
    private GuestNPC npc;
    private Transform camera;
    private void Start()
    {
        camera = Camera.main.transform;
        npc = this.gameObject.GetComponent<GuestNPC>();
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
            }
        }
    }
    private void EndOrder()
    {
        StopCoroutine(WaitingOrder());
        NPCUI.SetActive(false);
        currentOrderUI.EndOrder();
    }
    public void ReceiveFood(int ReceiveFood) //npc에게 음식 전달
    {
        if (ReceiveFood == foodInfos.ID && CanReceive)
        {
            Instantiate(foodInfos.PrefabFood, FoodPosition);
            npc.ChangeState(GuestNPC.State.Eat);
            StartCoroutine(ChangeWithDelay.CheckDelay(FoodData.Instance.EatTime, () => npc.ChangeState(GuestNPC.State.StandUP)));
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
    }

    public void AddObserver(GuestNPC obj) //MonoBehaviour 때문에 new 사용불가
    {
        obj.AddObserver(this);
    }

    public void Change(GuestNPC obj)
    {
        if (obj is GuestNPC)
        {
            var guestNPC = obj;
            if (obj.CurrentState == GuestNPC.State.StandUP)
            {
            }
            Debug.Log("observer");
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
