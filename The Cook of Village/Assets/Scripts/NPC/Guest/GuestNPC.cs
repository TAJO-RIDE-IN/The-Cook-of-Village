/////////////////////////////////////
/// 학번 : 91914200
/// 이름 : JungNaEun 정나은
////////////////////////////////////
using System.Collections.Generic;
using UnityEngine;

public interface IGuestDI
{
    void State(GuestNPC.State state);
}

public class Guest : IGuestDI
{
    public void State(GuestNPC.State state) { }
}

public interface IGuestOb
{
    void AddObserver(IObserver<GuestNPC> o);
    void RemoveObserver(IObserver<GuestNPC> o);
    void NotifyObserver();
}


public class GuestNPC : MonoBehaviour, IGuestOb
{
    IGuestDI guest;
    private List<IObserver<GuestNPC>> _observers = new List<IObserver<GuestNPC>>(); //ObserverList
    public enum State { Idle, Walk, Eat, Sit, StandUP, ChaseUP, Pay, GoOut }
    [SerializeField]
    private State currentState;
    public State CurrentState
    {
        get { return currentState; }
        set { currentState = value; }
    }
    public Animator ModelsAni;
    [SerializeField]
    private GameObject[] Models;
    private GameObject CurrentModel;
    private BoxCollider col;
    private void Start()
    {
        this.gameObject.GetComponent<GuestMove>().AddObserver();
        this.gameObject.GetComponent<FoodOrder>().AddObserver();
        col = this.gameObject.GetComponent<BoxCollider>();
    }
    #region Model 변경
    private void OnEnable()
    {
        SetNPCModel(true);
    }

    private void OnDisable()
    {
        SetNPCModel(false);
    }

    private void SetNPCModel(bool state)
    {
        if(state == true)
        {
            int model = Random.Range(0, Models.Length);
            CurrentModel = Models[model];
        }
        CurrentModel.SetActive(state);
        ModelsAni = CurrentModel.GetComponent<Animator>();
    }
    #endregion
    public void NPCAction()
    {
        switch (CurrentState)
        {
            case State.Idle:
                ModelsAni.SetBool("isWalk", false);
                break;
            case State.Walk:
                ModelsAni.SetBool("isWalk", true);
                break;
            case State.Eat:
                ModelsAni.SetBool("isEat", true);
                break;
            case State.Sit:
                col.enabled = true;
                ModelsAni.SetBool("isWalk", false);
                ModelsAni.SetTrigger("Sit");
                ModelsAni.SetTrigger("SitIdle");
                //StartCoroutine(ChangeWithDelay.CheckDelay(FoodData.Instance.FoodWaitingTime, () => ChangeState(State.ChaseUP)));
                break;
            case State.StandUP:
                col.enabled = false;
                ModelsAni.SetBool("isEat", false);
                ModelsAni.SetTrigger("StandUp");
                break;
            case State.ChaseUP:
                ModelsAni.SetTrigger("ChaseUp");
                break;
            case State.Pay:
                ModelsAni.SetTrigger("Pay");
                break;
            case State.GoOut:
                ObjectPooling<GuestNPC>.ReturnObject(this);
                break;
        }
    }
    public void AddGuestNPC(IGuestDI guest_) //MonoBehaviour 때문에 new 사용불가
    {
        guest = guest_;
    }
    public void ChangeState(State state)
    {
        guest.State(state);
        CurrentState = state;
        NPCAction(); // NPC상태에 따른 행동
        NotifyObserver(); //observer 전달     
    }
    #region Observer
    public void AddObserver(IObserver<GuestNPC> o) //ObserverList에 추가
    {
        _observers.Add(o);
    }
    public void RemoveObserver(IObserver<GuestNPC> o) //ObserverList에서 제거
    {
        _observers.Remove(o);
    }
    public void NotifyObserver() //observer에 값 전달
    {
        foreach (var observer in _observers)
        {
            observer.Change(this);
        }
    }
    #endregion
}
