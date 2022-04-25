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
    public GameObject ModelContainter;
    private GameObject[] Models;
    private GameObject CurrentModel;

    private void Awake()
    {
        //Models = ModelContainter.GetComponentsInChildren<GameObject>();
    }
    public void EatFood(int Price)
    {
        CurrentState = State.Eat;
        StartCoroutine(ChangeWithDelay.CheckDelay(FoodData.Instance.EatTime, () => PayFood(Price)));
    }

   /* private void OnEnable()
    {
        int model = Random.Range(0, Models.Length);
        CurrentModel = Models[model];
        CurrentModel.SetActive(true);

    }

    private void OnDisable()
    {
        CurrentModel.SetActive(false);
    }*/

    public void PayFood(int Price)
    {
        GameManager.Instance.Money += Price;
    }
}
