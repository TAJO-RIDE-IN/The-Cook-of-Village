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
    void AddObserver(IObserver o);
    void RemoveObserver(IObserver o);
    void NotifyObserver();
}
public interface IObserver
{
    void Change(GuestNPC obj);
}

public class GuestNPC : MonoBehaviour, IGuestOb
{
    IGuestDI guest;
    private List<IObserver> _observers = new List<IObserver>();
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
    private Animator ModelsAni;
    private void Start()
    {
        this.gameObject.GetComponent<GuestMove>().AddObserver();
        this.gameObject.GetComponent<FoodOrder>().AddObserver();
        Init();
    }
    private void Init()
    {
        CurrentState = State.Idle;
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
                break;
            case State.Walk:
                break;
            case State.Eat:
                break;
            case State.Sit:
                ModelsAni.SetTrigger("Sit");
                break;
            case State.StandUP:
                break;
            case State.ChaseUP:
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
        NotifyObserver(); //observer ����
        NPCAction();
    }
    #region Observer
    public void AddObserver(IObserver o)
    {
        _observers.Add(o);
    }
    public void RemoveObserver(IObserver o)
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
