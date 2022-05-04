/////////////////////////////////////
/// 학번 : 91914200
/// 이름 : JungNaEun 정나은
////////////////////////////////////
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class RestaurantNPC : MonoBehaviour
{
    public enum State { Idle, Walk, Sit, Eat }
    public State CurrentState = State.Idle;
    [SerializeField]
    private GameObject[] Models;
    private GameObject CurrentModel;

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

    private void NPCMove()
    {
        this.GetComponent<NavMeshAgent>().SetDestination(new Vector3(0f, 0f, 0f)); //vectorv potition으로 이동
    }
}
