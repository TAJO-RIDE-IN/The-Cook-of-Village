using System.Collections;
using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Potion : MonoBehaviour
{
    public float RedDuration = 60f;
    public float RedMutiple = 1.5f;
    public float OrangeDuration = 180f;
    public float OrangeMutiple = 1.5f;
    public float GreenReduction = 0.5f;

    //포션 사용상태
    public bool Red = false;
    public bool Orange = false;
    public bool Green = false;
    public bool Brown = false;

    private float RedTime = 0;
    private float OrangeTime = 0;

    private ThirdPersonGravity VillagePlayer;
    private ThirdPersonMovement RestaurantPlayer;
    private CounterQueue Counter;
    private List<CookingTool> Tool = new List<CookingTool>();

    private Coroutine RunningRed;
    private Coroutine RunningOrange;
    #region 싱글톤
    private static Potion instance = null;
    private void Awake() //씬 시작될때 인스턴스 초기화
    {
        if (null == instance)
        {
            instance = this;
            DontDestroyOnLoad(this.gameObject);
        }
        else
        {
            Destroy(this.gameObject);
        }
    }
    public static Potion Instance
    {
        get
        {
            if (null == instance)
            {
                return null;
            }
            return instance;
        }
    }
    #endregion
    #region OnSceneLoaded
    private void Start()
    {
        LoadObject();
    }
    void OnEnable()
    {
        // 델리게이트 체인 추가
        SceneManager.sceneLoaded += OnSceneLoaded;
    }
    void OnDisable()
    {
        // 델리게이트 체인 제거
        SceneManager.sceneLoaded -= OnSceneLoaded;
    }

    private void OnSceneLoaded(Scene scene, LoadSceneMode mode)
    {
        LoadObject();
    }
    private void LoadObject()
    {
        if (GameManager.Instance.CurrentSceneIndex == 2)
        {
            VillagePlayer = GameObject.FindGameObjectWithTag("Player").GetComponent<ThirdPersonGravity>();
            VillagePlayer.speed = VillagePlayer.OriginSpeed;
        }
        if (GameManager.Instance.CurrentSceneIndex == 3)
        {
            Tool.Clear();
            RestaurantPlayer = GameObject.FindGameObjectWithTag("Player").GetComponent<ThirdPersonMovement>();
            Counter = GameObject.FindGameObjectWithTag("Counter").GetComponent<CounterQueue>();
            GameObject[] CookTool = GameObject.FindGameObjectsWithTag("CookingTools");
            foreach(var tool in CookTool) {
                Tool.Add(tool.GetComponent<CookingTool>()); //CookingTool 리스트에 추가
            } 
            RestaurantPlayer.speed = RestaurantPlayer.OriginSpeed;
        }
        if (Red) {StopCoroutine(RunningRed); StartCoroutine(UseRedPotion()); }
    }
    #endregion
    public void ResetPotion() //하루지나면 포션 효과 제거
    {
        Red = false; Orange = false; Green = false; Brown = false;
        RedTime = 0; OrangeTime = 0;
        RedPlayerSpeed(1);
        Counter.PayMultiple = 1f;
        foreach (var CookingTool in Tool)
        {
            CookingTool.GreenPotionEffect = 1;
        }
    }
    public bool CanUsePotion(string potion)
    {
        switch (potion)
        {
            case "OrangePotion":
                return (GameManager.Instance.CurrentSceneIndex == 3); //레스토랑
            case "GreenPotion":
                return (GameManager.Instance.CurrentSceneIndex == 3 && !Green); //레스토랑
            case "BrownPotion":
                return (GameManager.Instance.CurrentSceneIndex == 2 && !Brown); //마을
            default: //Red, Rainbow 씬 상관없음
                return true;
        }
    } //정해진 씬에서만 사용가능, 사용 유무 확인

    #region Potion Effect
    public void UsePotion(string potion)
    {
        switch (potion)
        {
            case "RedPotion":
                RedTime += RedDuration;
                if (Red == true) { StopCoroutine(RunningRed);} //코루틴 중복방지
                RunningRed = StartCoroutine(UseRedPotion());
                break;
            case "OrangePotion":
                OrangeTime += OrangeDuration;
                if (Orange == true) { StopCoroutine(RunningOrange); } //코루틴 중복방지
                RunningOrange = StartCoroutine(UseOrangePotion());
                break;
            case "GreenPotion":
                UseGreenPotion();
                break;
            case "BrownPotion":
                UseBrownPotion();
                break;
            case "RainbowPotion":
                UseRainbowPotion();
                break;
        }
    }
    #region RedPotion
    private IEnumerator UseRedPotion() //이동속도 증가
    {
        Red = true;
        RedPlayerSpeed(RedMutiple);
        while (RedTime > 0)
        {
            RedTime -= Time.deltaTime;
            if(RedTime <= 0) //지속시간 종료
            {
                Red = false;
                RedPlayerSpeed(1);
            }
            yield return null;
        }
    }
    private void RedPlayerSpeed(float speed) //플레이어 속도 변경
    {
        if (GameManager.Instance.CurrentSceneIndex == 2)
        {
            VillagePlayer.speed = VillagePlayer.OriginSpeed * speed;
        }
        else if (GameManager.Instance.CurrentSceneIndex == 3)
        {
            RestaurantPlayer.speed = RestaurantPlayer.OriginSpeed * speed;
            Debug.Log(RestaurantPlayer.speed);
        }
    }
    #endregion
    private IEnumerator UseOrangePotion() //계산 가격 증가
    {
        Orange = true;
        Counter.PayMultiple = 1.5f;
        while (OrangeTime > 0)
        {
            OrangeTime -= Time.deltaTime;
            if (OrangeTime <= 0) //지속시간 종료
            {
                Counter.PayMultiple = 1f;
                Orange = false;
            }
            yield return null;
        }
    }
    private void UseGreenPotion() //조리시간 감소
    {
        foreach (var CookingTool in Tool)
        {
            CookingTool.GreenPotionEffect = GreenReduction;
        }
    }
    private void UseBrownPotion() //초콜릿 상인 소환
    {

    }
    private void UseRainbowPotion() //인벤토리 해금
    {
        GameData.Instance.RainbowDrinking++;
        int count = GameData.Instance.RainbowDrinking;
        if (count%5 == 0)
        {
            //해금
        }
    }
    #endregion
}
