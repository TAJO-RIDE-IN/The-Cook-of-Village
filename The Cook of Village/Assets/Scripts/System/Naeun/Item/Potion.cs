using System.Collections;
using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public interface IPotionOb
{
    void AddObserver(IObserver<Potion> o);
    void RemoveObserver(IObserver<Potion> o);
    void NotifyObserver();
}

public class Potion : Singletion<Potion>, IPotionOb
{
    public float RedDuration = 60f;
    public float RedEffectNum = 1.5f;
    public float OrangeDuration = 180f;
    public float OrangeEffectNum = 1.5f;
    public float GreenEffectNum = 2f;

    //포션 사용상태
    public bool Red = false;
    public bool Orange = false;
    public bool Green = false;
    public bool Brown = false;

    public bool PotionReset = false;

    public float RedTime = 0;
    public float OrangeTime = 0;

    private ThirdPersonGravity VillagePlayer;
    private ThirdPersonMovement RestaurantPlayer;
    private GameObject ChocolateShop;
    private CounterQueue Counter;
    private List<CookingTool> Tool = new List<CookingTool>();

    private Coroutine RunningRed;
    private Coroutine RunningOrange;

    private List<IObserver<Potion>> _observers = new List<IObserver<Potion>>();

    private void Start()
    {
        LoadObject();
    }
    public void LoadObject()
    {
        if (GameManager.Instance.CurrentSceneIndex == 2)
        {
            VillageSceneInit();
        }
        if (GameManager.Instance.CurrentSceneIndex == 3)
        {
            RestaurantSceneInit();
        }
        if (Red) { UseRedPotion(RedEffectNum); }
        NotifyObserver();
    }

    public void VillageSceneInit()
    {
        VillagePlayer = GameObject.FindGameObjectWithTag("Player").GetComponent<ThirdPersonGravity>();
        VillagePlayer.speed = VillagePlayer.OriginSpeed;
        UseBrownPotion(Brown);
    }
    public void RestaurantSceneInit()
    {
        Tool.Clear();
        Counter = FindObjectOfType<CounterQueue>();
        RestaurantPlayer = GameObject.FindGameObjectWithTag("Player").GetComponent<ThirdPersonMovement>();
        GameObject[] CookTool = GameObject.FindGameObjectsWithTag("CookingTools");
        foreach (var tool in CookTool)
        {
            Tool.Add(tool.GetComponent<CookingTool>()); //CookingTool 리스트에 추가
        }
        RestaurantPlayer.speed = RestaurantPlayer.OriginSpeed;
        if (PotionReset) { ResetPotion(); }
        if (!Orange) { UseOrangePotion(1f); }
    }
    public void ResetPotion() //하루지나면 포션 효과 제거
    {
        PotionReset = true;
        Red = false; Orange = false; Green = false; Brown = false;
        RedTime = 0; OrangeTime = 0;
        UseRedPotion(1);
        if(GameManager.Instance.CurrentSceneIndex == 3)
        {
            UseOrangePotion(1f);
            UseGreenPotion(1f);
            PotionReset = false;
        }
        NotifyObserver();
    }
    #region Potion Effect
    public void UsePotion(string potion)
    {
        switch (potion)
        {
            case "RedPotion":
                Red = true;
                RedTime += RedDuration;
                if (RunningRed != null) { StopCoroutine(RunningRed); }//코루틴 중복방지
                RunningRed = StartCoroutine(UseRedTime());
                UseRedPotion(RedEffectNum);
                break;
            case "OrangePotion":
                Orange = true;
                OrangeTime += OrangeDuration;
                if (RunningOrange != null) { StopCoroutine(RunningOrange); }//코루틴 중복방지
                RunningOrange = StartCoroutine(UseOrangeTime());
                break;
            case "GreenPotion":
                Green = true;
                UseGreenPotion(GreenEffectNum);
                break;
            case "BrownPotion":
                Brown = true;
                UseBrownPotion(Brown);
                break;
            case "RainbowPotion":
                UseRainbowPotion();
                break;
        }
        NotifyObserver();
    }
    #region RedPotion
    private IEnumerator UseRedTime()
    {
        while (RedTime > 0)
        {
            RedTime -= Time.deltaTime;
            if (RedTime <= 0) //지속시간 종료
            {
                Red = false;
                UseRedPotion(1);
            }
            NotifyObserver();
            yield return null;
        }
    }
    private void UseRedPotion(float speed) //플레이어 속도 변경
    {
        if (VillagePlayer != null)
        {
            VillagePlayer.speed = VillagePlayer.OriginSpeed * speed;
        }
        else if (RestaurantPlayer != null)
        {
            RestaurantPlayer.speed = RestaurantPlayer.OriginSpeed * speed;
        }
    }
    #endregion
    private IEnumerator UseOrangeTime()
    {
        while (OrangeTime > 0)
        {
            OrangeTime -= Time.deltaTime;
            if (OrangeTime <= 0) //지속시간 종료
            {
                UseOrangePotion(1f);
                Orange = false;
            }
            NotifyObserver();
            yield return null;
        }
    }
    private void UseOrangePotion(float effect) //계산 가격 증가
    {
        if(Counter != null)
        {
            Counter.PayMultiple = effect;
        }
    }
    private void UseGreenPotion(float effect) //조리시간 감소
    {
        if(Tool != null)
        {
            foreach (var CookingTool in Tool)
            {
                CookingTool.GreenPotionEffect = effect;
            }
        }
    }
    private void UseBrownPotion(bool state) //초콜릿 상인 소환
    {
        if(ChocolateShop != null)
        {
            ChocolateShop.SetActive(state);
        }
    }
    private void UseRainbowPotion() //인벤토리 해금
    {
        GameData.Instance.RainbowDrinking++;
        int count = GameData.Instance.RainbowDrinking;
        if (count%5 == 0)
        {
            ChefInventory.Instance.ExtensionInventory();
        }
    }
    #endregion

    public void AddObserver(IObserver<Potion> o)
    {
        _observers.Add(o);
    }
    public void RemoveObserver(IObserver<Potion> o)
    {
        _observers.Remove(o);
    }
    public void NotifyObserver() //observer에 값 전달
    {
        foreach (var observer in _observers)
        {
            observer.Change(this);
        }
    }
}
