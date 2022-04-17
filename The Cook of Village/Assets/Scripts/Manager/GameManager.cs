/////////////////////////////////////
/// 학번 : 91914200
/// 이름 : JungNaEun 정나은
////////////////////////////////////
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    private static GameManager instance = null;
    private void Awake() //씬 시작될때 인스턴스 초기화
    {
        if(null == instance)
        {
            instance = this;
            DontDestroyOnLoad(this.gameObject);
        }
        else
        {
            Destroy(this.gameObject);
        }    
    }
    public static GameManager Instance
    {
        get
        {
            if(null == instance)
            {
                return null;
            }
            return instance;
        }
    }

    public MoneyText moneyText;
    public TimeDayText timeDayText;
    [SerializeField]
    private float timeOfDay;
    public float TimeOfDay
    {
        get { return timeOfDay; }
        set 
        { 
            timeOfDay = value;
            timeDayText.ChangeTime(value);
        }
    }
    [SerializeField]
    private int day = 1;
    public int Day
    {
        get { return day; }
        set 
        { 
            day = value;
            timeDayText.ChangeDay(value);
        }
    }
    [SerializeField]
    private int money;
    public int Money
    {
        get { return money; }
        set 
        { 
            money = value;
            moneyText.ChangeMoney(value);
        }
    }
}
