using System.Collections;
using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Potion : MonoBehaviour
{
    public float RedDuration = 60f;
    public float OrangeDuration = 180f;

    public bool Red = false;
    public bool Orange = false;
    public bool Green = false;
    public bool Brown = false;

    private float RedTime = 0;
    private float OrangeTime = 0;

    private float OriginSpeed;
    private ThirdPersonGravity VillagePlayer;
    private ThirdPersonMovement RestaurantPlayer;

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
        if (GameManager.Instance.NextSceneIndex == 3)
        {
            VillagePlayer = GameObject.FindGameObjectWithTag("Player").GetComponent<ThirdPersonGravity>();
            OriginSpeed = VillagePlayer.speed;
        }
        else if (GameManager.Instance.NextSceneIndex == 4)
        {
            RestaurantPlayer = GameObject.FindGameObjectWithTag("Player").GetComponent<ThirdPersonMovement>();
            OriginSpeed = RestaurantPlayer.speed;
        }
    }
    #endregion
    public void UsePotion(string potion)
    {
        switch (potion)
        {
            case "RedPotion":
                if(Red == true) { StopCoroutine(RunningRed);} //코루틴 중복방지
                RunningRed = StartCoroutine(UseRedPotion());
                break;
            case "OrangePotion":
                StartCoroutine(UseOrangePotion());
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

    private IEnumerator UseRedPotion() //이동속도 증가
    {
        Red = true;
        RedTime += RedDuration;
        Debug.Log(RedTime);
        RedPlayerSpeed(OriginSpeed * 1.5f);
        while (RedTime > 0)
        {
            RedTime -= Time.deltaTime;
            if(RedTime <= 0) //지속시간 종료
            {
                Red = false;
                RedPlayerSpeed(OriginSpeed);
            }
            yield return null;
        }
    }
    private void RedPlayerSpeed(float speed)
    {
        if (GameManager.Instance.NextSceneIndex == 3)
        {
            VillagePlayer.speed = speed;
        }
        else if (GameManager.Instance.NextSceneIndex == 4)
        {
            RestaurantPlayer.speed = speed;
        }
    }
    private IEnumerator UseOrangePotion() //이동속도 증가
    {
        Orange = true;
        yield return null;
    }
    private void UseGreenPotion() //조리시간 감소
    {

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
}
