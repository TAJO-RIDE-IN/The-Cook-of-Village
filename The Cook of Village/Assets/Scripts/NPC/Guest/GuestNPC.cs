/////////////////////////////////////
/// 학번 : 91914200
/// 이름 : JungNaEun 정나은
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

    public GuestNPC(IGuestDI guest_)
    {
        guest = guest_;
    }

    public enum State {Idle, Walk, Eat, Sit, StandUP}
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

    private void Start()
    {
        this.gameObject.AddComponent<GuestMove>().AddObserver();
    }
    #region Model 변경
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
    }
    #endregion
    public void NPCState()
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
                break;
            case State.StandUP:
                break;
        }
    }

    public void ChangeState(State state)
    {
        guest.State(state);
        CurrentState = state;
        NotifyObserver();
        NPCState();
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
    public void NotifyObserver()
    {
        foreach(var observer in _observers)
        {
            observer.Change(this);
        }
    }
    #endregion
}
