/////////////////////////////////////
/// �й� : 91914200
/// �̸� : JungNaEun ������
////////////////////////////////////
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public interface IGuestDI
{
    void State(GuestNPC.State state);
}

public class Guest : IGuestDI
{
    public void State(GuestNPC.State state){ }
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
    private List<IObserver<GuestNPC>> _observers = new List<IObserver<GuestNPC>>();
    public enum State {Idle, Walk, Eat, Sit, StandUP, ChaseUP, Pay, GoOut }
    [SerializeField]
    private State currentState;
    public State CurrentState
    {
        get{ return currentState; }
        set
        {
            currentState = value;
        }
    }
    [SerializeField]
    private GameObject[] Models;
    private GameObject CurrentModel;
    public Animator ModelsAni;
    private void Start()
    {
        this.gameObject.GetComponent<GuestMove>().AddObserver();
        this.gameObject.GetComponent<FoodOrder>().AddObserver();
    }
    #region Model ����
    private void OnEnable()
    {
        SetNPCModel(true);
    }

    private void OnDisable()
    {
        CurrentModel.SetActive(false);
    }

    private void SetNPCModel(bool state)
    {
        if (true)
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
                ModelsAni.SetBool("isWalk", false);
                ModelsAni.SetTrigger("Sit");
                ModelsAni.SetTrigger("SitIdle");
                break;
            case State.StandUP:
                ModelsAni.SetBool("isEat", false);
                ModelsAni.SetTrigger("StandUp");
                break;
            case State.ChaseUP:
                ModelsAni.SetTrigger("ChaseUp");
                break;
            case State.Pay:
                break;
            case State.GoOut:
                ObjectPooling<GuestNPC>.ReturnObject(this);
                break;
        }
    }
    public void AddGuestNPC(IGuestDI guest_) //MonoBehaviour ������ new ���Ұ�
    {
        guest = guest_;
    }
    public void ChangeState(State state)
    {
        guest.State(state);
        CurrentState = state;
        NPCAction();
        NotifyObserver(); //observer ����     
    }
    #region Observer
    public void AddObserver(IObserver<GuestNPC> o)
    {
        _observers.Add(o);
    }
    public void RemoveObserver(IObserver<GuestNPC> o)
    {
        _observers.Remove(o);
    }
    public void NotifyObserver() //observer�� �� ����
    {
        foreach(var observer in _observers)
        {
            observer.Change(this);
        }
    }
    #endregion
}
