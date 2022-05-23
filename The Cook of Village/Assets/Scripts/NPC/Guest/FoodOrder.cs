/////////////////////////////////////
/// 학번 : 91914200
/// 이름 : JungNaEun 정나은
////////////////////////////////////
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class FoodOrder : MonoBehaviour, IObserver<GuestNPC>
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
    private bool Receive = false;
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
    private void OnEnable()
    {
        Receive = false; //Receive bool 초기화
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
        float time = FoodData.Instance.FoodWaitingTime;
        while(time > 0)
        {
            time -= Time.deltaTime;
            float ratio = time / FoodData.Instance.FoodWaitingTime;
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
        currentOrderUI.EndOrder(); //주문서
        if(Receive == false)
        {
            guest.ChangeState(GuestNPC.State.StandUP);
        }
    }
    private void EndEat()
    {
        guest.ChangeState(GuestNPC.State.StandUP);
        if(FoodPosition.GetChild(0) != null)
        {
            Destroy(FoodPosition.GetChild(0).gameObject);
        }
    }
    public bool ReceiveFood(int ReceiveFood) //npc에게 음식 전달
    {
        if (ReceiveFood == foodInfos.ID && CanReceive) 
        {
            Receive = true;
            Instantiate(foodInfos.PrefabFood, FoodPosition);
            guest.ChangeState(GuestNPC.State.Eat);
            EndOrder();
            StartCoroutine(ChangeWithDelay.CheckDelay(FoodData.Instance.EatTime, () => EndEat())); //일정 시간 후 다 먹음
            return Receive;
        }
        return Receive;
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
    public void PayFood()
    {
        GameData.Instance.Money += foodInfos.Price;
        foodInfos = null;
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
