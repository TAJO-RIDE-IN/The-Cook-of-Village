using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

[System.Serializable]
public class NPCUIImage
{
    public Sprite OrderWaitImage;
    public Sprite MoneyImage;
}

public class FoodOrder : MonoBehaviour, IObserver<GuestNPC>
{
    Probability<FoodInfos> FoodProbability = new Probability<FoodInfos>();
    [SerializeField]
    private NPCUIImage NPCImage;
    [SerializeField]
    private FoodInfos foodInfos;
    public GameObject NPCUI;
    public Transform FoodPosition;
    public Image RemainingTimeImage;
    public Image OrderFoodImage;

    private bool CanReceive = false;
    private OrderUI currentOrderUI;
    private GuestNPC guest;
    private new Transform camera;
    private Coroutine WaitingOrderCoroutine;
    private bool Receive = false;
    private void Awake()
    {
        camera = Camera.main.transform;
        guest = this.gameObject.GetComponent<GuestNPC>();
        guest.AddGuestNPC(new Guest());
        AddProbability();
    }
    private void Update() //손님 머리위 UI가 항상 카메라를 보도록 함.
    {
        NPCUI.transform.LookAt(NPCUI.transform.position + camera.rotation * Vector3.forward, camera.rotation * Vector3.up);
    }
    private void OnEnable()
    {
        RemainingTimeImage.fillAmount = 0;
        Receive = false; //Receive bool 초기화
        foodInfos = null; // Food 정보 초기화
    }
    private void AddProbability() //확률 가중치 부여
    {
        foreach(FoodTool i in FoodData.Instance.foodTool)
        {
            if(i.Amount != 0)
            {
                foreach (FoodInfos j in i.foodInfos)
                {
                    FoodProbability.Add(j, j.OrderProbability);
                }
            }
        }
    }
    private IEnumerator WaitingOrder() //주문 기다림
    {
        float time = FoodData.Instance.FoodWaitingTime;
        while(time > 0)
        {
            time -= Time.deltaTime;
            float ratio = time / FoodData.Instance.FoodWaitingTime;
            RemainingTimeImage.fillAmount = 1 - ratio;
            currentOrderUI.TimeBar.fillAmount = 1 - ratio;
            if (ratio <= 0.4)
            {
                currentOrderUI.OrderAnimation(true);
            }
            if (time <= 0)
            {
                currentOrderUI.OrderAnimation(false);
                EndOrder();
            }
            yield return null;
        }
    }
    private void EndOrder() //주문종료
    {
        StopCoroutine(WaitingOrderCoroutine);
        NPCUI.SetActive(false);
        currentOrderUI.EndOrder(); //주문서
        if(Receive == false)
        {
            guest.ChangeState(GuestNPC.State.StandUP);
        }
    }
    private void EndEat() //음식 다 먹음
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
 
    private void Order() //음식주문
    {
        foodInfos = FoodProbability.Get(); //음식 확률에 따라 고르기
        OrderFoodImage.sprite = foodInfos.ImageUI;
        currentOrderUI = ObjectPooling<OrderUI>.GetObject(); //화면 상단 주문서 표시
        currentOrderUI.foodInfos = foodInfos;
        WaitingOrderCoroutine = StartCoroutine(WaitingOrder());
    }
    public void PayFood(float multiple) //계산
    {
        GameData.Instance.Money += foodInfos.Price * multiple;
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
                NPCUI.SetActive(true);
                OrderFoodImage.sprite = NPCImage.OrderWaitImage;
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
