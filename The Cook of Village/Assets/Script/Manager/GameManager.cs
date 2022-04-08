using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    private static GameManager instance = null;
    private void Awake() //�� ���۵ɶ� �ν��Ͻ� �ʱ�ȭ
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
