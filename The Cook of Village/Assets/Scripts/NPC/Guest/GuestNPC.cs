/////////////////////////////////////
/// 학번 : 91914200
/// 이름 : JungNaEun 정나은
////////////////////////////////////
using System.Collections.Generic;
using UnityEngine;

public interface IGuestOb
{
    void AddObserver(IObserver<GuestNPC> o);
    void RemoveObserver(IObserver<GuestNPC> o);
    void NotifyObserver();
}


public class GuestNPC : MonoBehaviour, IGuestOb
{
    private List<IObserver<GuestNPC>> _observers = new List<IObserver<GuestNPC>>(); //ObserverList
    public enum Guest { General, Villge }
    public Guest currentNPC;
    public enum State { Idle, Walk, Eat, Sit, StandUP, ChaseUP, Pay, GoOut }
    [SerializeField]
    private State currentState;
    public State CurrentState
    {
        get { return currentState; }
        set { currentState = value; }
    }

    [SerializeField] private GameObject[] Models;
    private GameObject CurrentModel;
    public Animator ModelsAni;
    public new BoxCollider collider;
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
                collider.enabled = true;
                ModelsAni.SetBool("isWalk", false);
                ModelsAni.SetTrigger("Sit");
                ModelsAni.SetTrigger("SitIdle");
                //StartCoroutine(ChangeWithDelay.CheckDelay(FoodData.Instance.FoodWaitingTime, () => ChangeState(State.ChaseUP)));
                break;
            case State.StandUP:
                collider.enabled = false;
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

    public void ChangeState(State state)
    {
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
