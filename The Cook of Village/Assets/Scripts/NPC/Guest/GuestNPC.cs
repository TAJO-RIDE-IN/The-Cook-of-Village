/////////////////////////////////////
/// 학번 : 91914200
/// 이름 : JungNaEun 정나은
////////////////////////////////////
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public interface IGuest
{
    void AddObserver(IObserver o);
    void RemoveObserver(IObserver o);
    void NotifyObserver();
}
public interface IObserver
{
    void Change(GuestNPC obj);
}

public class GuestNPC : MonoBehaviour, IGuest
{
    private List<IObserver> _observers = new List<IObserver>();
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

    private void Start()
    {
        this.gameObject.GetComponent<GuestMove>().AddObserver(this);
    }

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
        CurrentState = state;
        NotifyObserver();
        NPCState();
    }

    public void AddObserver(IObserver o)
    {
        this._observers.Add(o);
    }
    public void RemoveObserver(IObserver o)
    {
        this._observers.Remove(o);
    }
    public void NotifyObserver()
    {
        foreach(var observer in this._observers)
        {
            observer.Change(this);
        }
    }
}
