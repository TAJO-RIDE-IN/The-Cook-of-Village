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
    [SerializeField] private Animator ani;
    private void Start()
    {
        npcInfos = NPCData.Instance.npcInfos[(int)npcInfos.work];
        AddObserver(GameData.Instance);
    }
    public void OnTriggerEnter(Collider other)
    {
        if (other.tag == "Player")
        {

            isOpen = true;
            CurrentState = State.Greet;
            EnterShop();
        }
    }
    public void OnTriggerExit(Collider other)
    {
        if (other.tag == "Player")
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
        this.gameObject.GetComponent<Collider>().enabled = state;
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
            bool open = (GameData.Today == npcInfos.Holiday) ? false : true;
            ShopState(TodayShopOpen(obj.TimeOfDay, open));
        }
    }
    #endregion
}
