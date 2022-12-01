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
    public Image RemainingTimeImage;
    public Image OrderFoodImage;
    public Image VillageNPCImage;
    private OrderUI currentOrderUI;
    private GuestNPC guest;

    private new Transform camera;
    private Coroutine WaitingOrderCoroutine;
    private bool Receive = false;
    public bool CanReceive = false;

    //마을 주민
    private VillageGuest VillageNPC;
    private bool Village = false;
    private bool VillageEatFavorite = false;
    private int PayMoney;

    private FoodData foodData;
    private void Awake()
    {
        guest = GetComponent<GuestNPC>();
        if (guest.currentNPC == GuestNPC.Guest.Villge) 
        { 
            Village = true;
            VillageNPC = GetComponent<VillageGuest>();
        }
        camera = Camera.main.transform;
        foodData = FoodData.Instance;
        AddObserver(guest);
        AddProbability();
    }
    private void Update() //손님 머리위 UI가 항상 카메라를 보도록 함.
    {
        NPCUI.transform.LookAt(NPCUI.transform.position + camera.rotation * Vector3.forward, camera.rotation * Vector3.up);
    }
    private void OnEnable()
    {
        PayMoney = 0;
        RemainingTimeImage.fillAmount = 0;
        Receive = false; //Receive bool 초기화
        foodInfos = null; // Food 정보 초기화
        VillageNPCImage.gameObject.SetActive(false);
    }
    private void AddProbability() //확률 가중치 부여
    {
        foreach(FoodTool i in foodData.foodTool)
        {
            if(i.CanUse)
            {
                foreach (FoodInfos j in i.foodInfos)
                {
                    FoodProbability.Add(j, j.OrderProbability);
                }
            }
        }
    }
    private float SaveWaitTime;
    private float FoodWaitTime;
    private IEnumerator WaitingOrder() //주문 기다림
    {
        float Waitingtime = FoodWaitTime;
        while(FoodWaitTime > 0)
        {
            FoodWaitTime -= Time.deltaTime;
            float ratio = FoodWaitTime / Waitingtime;
            RemainingTimeImage.fillAmount = 1 - ratio;
            currentOrderUI.TimeBar.fillAmount = 1 - ratio;
            if (ratio <= 0.4)
            {
                currentOrderUI.OrderAnimation(true);
                guest.ChangeState(GuestNPC.State.ChaseUP);
            }
            if (FoodWaitTime <= 0)
            {
                currentOrderUI.OrderAnimation(false);
                if(Village && !VillageEatFavorite)
                {
                    MoneyData.Instance.TipCount = 0;
                }
                EndOrder();
            }
            yield return null;
        }
    }
    private void EndOrder() //주문종료
    {
        if(WaitingOrderCoroutine != null)
        {
            StopCoroutine(WaitingOrderCoroutine);
        }
        NPCUI.SetActive(false);
        currentOrderUI.EndOrder(); //주문서
        if(!Receive)
        {
            guest.ChangeState(GuestNPC.State.StandUP);
            guest.ChangeState(GuestNPC.State.ChaseUP);
            GameData.Instance.GuestCountData(-1);
        }
    }
    protected virtual void EndEat() //음식 다 먹음
    {
        guest.ChangeState(GuestNPC.State.StandUP);
        guest.chairUse.FoodEnable(foodInfos.ID, false);
    }
    public bool ReceiveFood(int ReceiveFood) //npc에게 음식 전달
    {
        if (ReceiveFood.Equals(foodInfos.ID) && CanReceive) 
        {
            CanReceive = false;
            Receive = true;
            guest.chairUse.FoodEnable(foodInfos.ID, true); //책상에 음식 활성화
            guest.isDrink = foodData.DrinkFood((int)foodInfos.Type); //마시는 음식인 경우 소리 다르게 해야하기 때문에 확인
            guest.ChangeState(GuestNPC.State.Eat);
            EndOrder();
            PayMoney += foodInfos.Price;
            if (Village) 
            {
                VillageReceive();
                return Receive; 
            }
            StartCoroutine(ChangeWithDelay.CheckDelay(foodData.EatTime, () => EndEat())); //일정 시간 후 다 먹음
            return Receive;
        }
        else
        {
            FoodWaitTime -= SaveWaitTime * 0.2f; //패널티 : 음식 기다리는 시간이 10퍼센트 줄음
            guest.ChangeState(GuestNPC.State.ChaseUP);
            MoneyData.Instance.TipCount = 0;
        }
        return Receive;
    }
    
    private void VillageReceive()
    {
        if (Village && !VillageEatFavorite)
        {
            VillageEatFavorite = true;
            SoundManager.Instance.StopEffect3D(this.gameObject); //먹는 소리 off
            StartCoroutine(ChangeWithDelay.CheckDelay(foodData.EatTime, () => Order(foodData.Foodinfos(VillageNPC.npcInfos.FavoriteFood))));
        }
        else if(Village && VillageEatFavorite)
        {
            VillageNPC.npcInfos.Likeability += 20;
            VillageNPC.npcInfos.EatFavriteFood = true;
            StartCoroutine(ChangeWithDelay.CheckDelay(foodData.EatTime, () => EndEat())); //일정 시간 후 다 먹음
        }
    }
    protected virtual void Order(FoodInfos infos) //음식주문
    {
        guest.OrderSound();
        OrderInit(infos);
        FoodWaitTime = WaitTime();
        SaveWaitTime = FoodWaitTime;
        WaitingOrderCoroutine = StartCoroutine(WaitingOrder());
    }
    private float WaitTime()
    {
        float time = foodData.DefaultWaitingTime + foodInfos.MakeTime * 2f;
        if (this.gameObject.layer.Equals(7))
        {
            time += 20f;
            Debug.Log(time);
        }
        return time;
    }
    protected void OrderInit(FoodInfos infos)
    {
        if (foodInfos != null)
        {
            guest.chairUse.FoodEnable(foodInfos.ID, false);
        }
        NPCUI.SetActive(true);
        Receive = false;
        CanReceive = true;
        foodInfos = infos;
        ChangeOrderImage();
    }
    private void ChangeOrderImage()
    {
        Color orderImageColor = new Color(1, 1, 1, 1);
        bool favorite = false;
        if (VillageNPC != null && VillageEatFavorite)
        {
            favorite = true;
            VillageNPCImage.gameObject.SetActive(true);
            VillageNPCImage.sprite = ImageData.Instance.FindImageData(VillageNPC.npcInfos.ImageID);
            if (!VillageNPC.npcInfos.EatFavriteFood)
            {
                orderImageColor = VillageNPC.VillageFoodColor;
            } 
        }
        OrderFoodImage.sprite = ImageData.Instance.FindImageData(foodInfos.ImageID);
        OrderFoodImage.color = orderImageColor;
        currentOrderUI = ObjectPooling<OrderUI>.GetObject(); //화면 상단 주문서 표시
        currentOrderUI.ModifyOrderUI(foodInfos, favorite, VillageNPC);
    }
    public virtual void PayFood(float multiple) //계산
    {
        GameData.Instance.ChangeFame(+3);
        GameData.Instance.GuestCountData(1);
        MoneyData.Instance.TipCount++;
        MoneyData.Instance.Money += (int)(PayMoney * multiple);
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
            if (guestNPC.CurrentState.Equals(GuestNPC.State.Sit))
            {
                StartCoroutine(ChangeWithDelay.CheckDelay(foodData.OrderTime, () => Order(FoodProbability.Get()))); //음식 확률에 따라 고르기
                NPCUI.SetActive(true);
                OrderFoodImage.sprite = guest.NPCImage.OrderWaitImage;
            }
        }
    }
}
