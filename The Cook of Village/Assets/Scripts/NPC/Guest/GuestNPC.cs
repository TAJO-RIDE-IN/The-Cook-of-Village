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
    void AddObsever(IObserver o);
    void RemoveObserver(IObserver o);
    void NotifyObserver();
}
public interface IObserver
{
    void Change(Object obj);
}

public class GuestNPC : MonoBehaviour, IGuest
{
    private List<IObserver> _observers = new List<IObserver>();
    protected enum State {Idle, Walk, Eat, Sit, StandUP}
    [SerializeField]
    private State currentState;
    protected State CurrentState
    {
        get{ return currentState; }
        set
        {
            currentState = value;
            NPCState();
        }
    }

    public void PayFood(int Price)
    {
        CurrentState = State.StandUP;
        GameManager.Instance.Money += Price;
    }

    private void NPCState()
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
    public void AddObsever(IObserver o)
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
}
