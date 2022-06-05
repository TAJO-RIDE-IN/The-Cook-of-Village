using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class realMove : MonoBehaviour
{
    // Use this for initialization
    float Times;
    void Start()
    {
        Times = 0.0f;
    }

    // Update is called once per frame
    void Update()
    {
        moveObjectFunc();
    }

    private float speed_move = 3.0f;
    private float speed_rota = 0.12f;

    float checkTime = 17.0f;
    float checkTime2 = 76.0f;
    float checkTime3 = 93.0f;
    float checkTime4 = 152.0f;
    void moveObjectFunc()
    {
        Times += Time.deltaTime;

        if (Times < checkTime)
        {
            transform.Translate(Vector3.right * speed_move * Time.deltaTime);
        }
        else if (Times < checkTime2)
        {
            transform.Translate(Vector3.down * speed_move * Time.deltaTime);
            transform.Translate(Vector3.back * speed_move * Time.deltaTime);
        }
        else if (Times < checkTime3)
        {
            transform.Translate(Vector3.left * speed_move * Time.deltaTime);
        }
        else if (Times < checkTime4)
        {
            transform.Translate(Vector3.up * speed_move * Time.deltaTime);
            transform.Translate(Vector3.forward * speed_move * Time.deltaTime);
        }
        else
        {
            Times = 0.0f;
        }
    }
}