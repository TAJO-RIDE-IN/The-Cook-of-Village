/////////////////////////////////////
/// �й� : 91914200
/// �̸� : JungNaEun ������
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
