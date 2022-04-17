/////////////////////////////////////
/// 학번 : 91914200
/// 이름 : JungNaEun 정나은
////////////////////////////////////
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RestaurantNPC : MonoBehaviour
{
    public enum State {Idle, Walk, Sit, Eat}
    public State CurrentState = State.Idle;

    public void EatFood(int Price)
    {
        CurrentState = State.Eat;
        StartCoroutine(ChangeWithDelay.CheckDelay(FoodData.Instance.EatTime, () => PayFood(Price)));
    }
    public void PayFood(int Price)
    {
        GameManager.Instance.Money += Price;
    }
}
