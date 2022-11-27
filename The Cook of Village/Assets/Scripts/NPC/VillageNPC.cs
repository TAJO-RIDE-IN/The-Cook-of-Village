using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class VillageNPC : MonoBehaviour, IObserver<GameData>
{
    public NPCInfos npcInfos;
    public enum State { Idle, Walk, Sell, Greet }
    [SerializeField]
    private State NPCState;
    public State CurrentState
    {
        get { return NPCState; }
        set
        {
            NPCState = value;
            NPCAnimation();
        }
    }
    public bool isOpen;
    private bool isTimeOpen;
    [SerializeField] private Animator ani;
    private void Start()
    {
        npcInfos = NPCData.Instance.npcInfos[(int)npcInfos.work];
        AddObserver(GameData.Instance);
    }
    public void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player") && isTimeOpen)
        {

            isOpen = true;
            CurrentState = State.Greet;
            EnterShop();
        }
    }
    public void OnTriggerExit(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            isOpen = false;
            CurrentState = State.Greet;
        }
    }
    protected bool TodayShopOpen(float time, bool open)
    {
        if (time >= npcInfos.OpenTime && time <= npcInfos.CloseTime)
        {
            if (open) return true;
            return false;
        }
        else
        {
            isOpen = false;
            return false;
        }
    }

    protected void NPCAnimation()
    {
        switch (CurrentState)
        {
            case State.Idle:
                break;
            case State.Walk:
                ani.SetTrigger("Walk");
                break;
            case State.Sell:
                ani.SetTrigger("Sell");
                CurrentState = State.Idle;
                break;
            case State.Greet:
                ani.SetTrigger("Greet");
                CurrentState = State.Idle;
                break;
        }
    }
    public abstract void UIState(bool state);
    protected void ShopState(bool state)
    {
        this.gameObject.SetActive(state);
    }
    public abstract void EnterShop();

    #region observer
    public void AddObserver(IGameDataOb o)
    {
        o.AddObserver(this);
    }
    public void RemoveObserver(IGameDataOb o)
    {
        o.RemoveObserver(this);
    }
    public void Change(GameData obj)
    {
        if (obj is GameData)
        {
            var GameData = obj;
            bool open;
            if (npcInfos.work.Equals(NPCInfos.Work.ChocolateShop))//일주일에 한 번만 문 열기 때문에 쉬는 날을 여는 날로 함
            {
                open = true;
                if (!Potion.Brown) //갈색 포션 사용 시 초콜렛 상점 주인이 사라지는 것을 방지
                {
                    open = (GameData.Today.Equals(npcInfos.Holiday)) ? true : false;
                }
            }
            else
            {
                open = (GameData.Today.Equals(npcInfos.Holiday)) ? false : true;
            }
            isTimeOpen = open;
            ShopState(TodayShopOpen(obj.TimeOfDay, open));
        }
    }
    #endregion
}
