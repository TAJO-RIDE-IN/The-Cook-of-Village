/////////////////////////////////////
/// 학번 : 91914200
/// 이름 : JungNaEun 정나은
////////////////////////////////////
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class RestaurantNPC : AIPosition
{
    public enum State {Idle, Walk, Eat, Sit, StandUP}
    private State currentState;
    public State CurrentState
    {
        get{ return currentState; }
        set
        {
            currentState = value;
            NPCState();
        }
    }
    [SerializeField]
    private GameObject[] Models;
    private GameObject CurrentModel;
    private NavMeshAgent agent;
    private GameObject DestinationChiar;
    private bool isArrive = false;

    private void Start()
    {
        agent = this.gameObject.GetComponent<NavMeshAgent>();
        StartCoroutine(NPCMove(ChairPosition()));
    }

    public void EatFood(int Price)
    {
        CurrentState = State.Eat;
        StartCoroutine(ChangeWithDelay.CheckDelay(FoodData.Instance.EatTime, () => PayFood(Price)));
    }
    private void OnEnable()
    {
        int model = Random.Range(0, Models.Length);
        CurrentModel = Models[model];
        CurrentModel.SetActive(true);
    }

    private void OnDisable()
    {
        CurrentModel.SetActive(false);
    }

    public void PayFood(int Price)
    {
        GameManager.Instance.Money += Price;
    }

    private IEnumerator NPCMove(Vector3 destination)
    {
        while(!isArrive)
        {
            agent.SetDestination(destination);
            if(agent.velocity.sqrMagnitude >= 0.2f &&  agent.remainingDistance <= 0.5f)
            {
                Debug.Log("도착");
                isArrive = true;
            }
            yield return null;
        }
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
}
