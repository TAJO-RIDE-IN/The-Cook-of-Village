using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class VillageNPC : MonoBehaviour, IObserver<GameData>
{
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
    private Animator ani;
    public bool isOpen;
    [SerializeField] protected int OpenTime;
    [SerializeField] protected int CloseTime;
    [SerializeField] protected int CloseDay;
    private void Start()
    {
        AddObserver(GameData.Instance);
        ani = this.gameObject.GetComponent<Animator>();
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
        if (time >= OpenTime && time <= CloseTime)
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
    private void OnDisable()
    {
        RemoveObserver(GameData.Instance);
    }

    public void AddObserver(IGameDataOb o)
    {
        o.AddObserver(this);
    }

    public void RemoveObserver(IGameDataOb o)
    {
        if (o != null) { o.RemoveObserver(this); }
    }

    public void Change(GameData obj)
    {
        if (obj is GameData)
        {
            var GameData = obj;
            bool open = (GameData.Today == CloseDay) ? false : true;
            ShopState(TodayShopOpen(obj.TimeOfDay, open));
        }
    }
    #endregion
}
