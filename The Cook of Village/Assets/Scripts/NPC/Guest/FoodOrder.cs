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
    private GameObject ReceiveFoodObject;
    public Image RemainingTimeImage;
    public Image OrderFoodImage;

    private OrderUI currentOrderUI;
    private GuestNPC guest;

    private new Transform camera;
    private Coroutine WaitingOrderCoroutine;
    private bool Receive = false;
    private bool CanReceive = false;

    //마을 주민
    private VillageGuest VillageNPC;
    private bool Village = false;
    private int VillageEatCount = 0;
    private void Awake()
    {
        guest = this.gameObject.GetComponent<GuestNPC>();
        if (guest.currentNPC == GuestNPC.Guest.Villge) 
        { 
            Village = true;
            VillageNPC = this.gameObject.GetComponent<VillageGuest>();
        }
        camera = Camera.main.transform;
        AddObserver(guest);
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
        VillageEatCount = 0;
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
        float time = foodInfos.MakeTime * 3f;
        while(time > 0)
        {
            time -= Time.deltaTime;
            float ratio = time / (foodInfos.MakeTime * 3f);
            RemainingTimeImage.fillAmount = 1 - ratio;
            currentOrderUI.TimeBar.fillAmount = 1 - ratio;
            if (ratio <= 0.4)
            {
                currentOrderUI.OrderAnimation(true);
                guest.ChangeState(GuestNPC.State.ChaseUP);
            }
            if (time <= 0)
            {
                currentOrderUI.OrderAnimation(false);
                GameData.Instance.TipCount = 0;
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
            guest.ChangeState(GuestNPC.State.ChaseUP);
        }
    }
    private void EndEat() //음식 다 먹음
    {
        guest.ChangeState(GuestNPC.State.StandUP);
        if (ReceiveFoodObject != null)
        {
            Destroy(ReceiveFoodObject.gameObject);
        }
    }
    public bool ReceiveFood(int ReceiveFood) //npc에게 음식 전달
    {
        if (ReceiveFood == foodInfos.ID && CanReceive) 
        {
            Receive = true;
            ReceiveFoodObject = Instantiate(foodInfos.PrefabFood, FoodPosition);
            VillageEatCount++;
            guest.ChangeState(GuestNPC.State.Eat);
            EndOrder();
            if (Village && VillageEatCount == 1)
            {
                StartCoroutine(ChangeWithDelay.CheckDelay(FoodData.Instance.EatTime, () => Order(FoodData.Instance.Foodinfos(VillageNPC.FavoriteFoodID))));
                return Receive;
            }
            StartCoroutine(ChangeWithDelay.CheckDelay(FoodData.Instance.EatTime, () => EndEat())); //일정 시간 후 다 먹음
            return Receive;
        }
        else
        {
            GameData.Instance.TipCount = 0;
        }
        return Receive;
    }
 
    private void Order(FoodInfos infos) //음식주문
    {
        if (ReceiveFoodObject != null)
        {
            Destroy(ReceiveFoodObject.gameObject);
        }
        NPCUI.SetActive(true);
        Receive = false;
        foodInfos = infos;
        OrderFoodImage.sprite = foodInfos.ImageUI;
        currentOrderUI = ObjectPooling<OrderUI>.GetObject(); //화면 상단 주문서 표시
        currentOrderUI.foodInfos = foodInfos;
        WaitingOrderCoroutine = StartCoroutine(WaitingOrder());
    }
    public void PayFood(float multiple) //계산
    {
        GameData.Instance.TipCount++;
        GameData.Instance.Money += (int)(foodInfos.Price * multiple);
        if(GameData.Instance.TipCount >= 5)
        {
            GameData.Instance.Money += GameData.Instance.TipMoney;
        }
        foodInfos = null;
        guest.ChangeState(GuestNPC.State.Pay);
    }

    public void AddObserver(IGuestOb o)
    {
        o.AddObserver(this);
    }
    public void RemoveObserver(IGuestOb o)
    {
        o.RemoveObserver(this);
    }
    public void Change(GuestNPC obj)
    {
        if (obj is GuestNPC)
        {
            var guestNPC = obj;
            if (guestNPC.CurrentState == GuestNPC.State.Sit)
            {
                StartCoroutine(ChangeWithDelay.CheckDelay(FoodData.Instance.OrderTime, () => Order(FoodProbability.Get()))); //음식 확률에 따라 고르기
                NPCUI.SetActive(true);
                OrderFoodImage.sprite = guest.NPCImage.OrderWaitImage;
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
