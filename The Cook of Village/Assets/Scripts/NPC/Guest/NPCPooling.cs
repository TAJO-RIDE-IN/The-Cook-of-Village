using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class NPCPooling : ObjectPooling<GuestNPC>, IObserver<GameData>
{
    [SerializeField] private List<VillageGuest> VillgeNPC = new List<VillageGuest>();
    public List<GameObject> WaitChair = new List<GameObject>();
    public List<GameObject> UseChair = new List<GameObject>();

    public GameObject NoticeUI;

    [SerializeField]
    private float CallTime = 20f;
    private float DefaultCallTime = 20f;
    private float FirstCallTime = 4f;
    private bool FirstNPC = false;
    private bool callVillageNPC = false;
    private Coroutine _callNPC;
    private float VillageNPCTime;
    private float OpenTime;

    private void Start()
    {
        AddObserver(GameData.Instance);
    }

    private void Update()
    {
        //일시정지 상태가 아닐 때, isOpen에 따라
        if (Input.GetKeyDown(KeyCode.P))
        {
            if (Time.timeScale != 0.0f)
            {
                NoticeUI.SetActive(false);
                SoundManager.Instance.Play(SoundManager.Instance._audioClips["OpenRestaurant"]);
                OpenRestaurant();
            }
            else
            {
                NoticeUI.SetActive(true);
                NoticeUI.SetActive(false);
                CloseRestaurant();
            }
            
        }
    }


    public void OpenRestaurant()
    {
        GameManager.Instance.IsOpen = true;
        FirstNPC = true;
        OpenTime = GameData.Instance.TimeOfDay;
        VillageNPCTime = OpenTime + Random.Range(60, 181); // 마을 주민 오는시간 -> 오픈 후 1시간~3시간 사이

        if(_callNPC != null)
        {
            StopCoroutine(_callNPC);
        }
        _callNPC = StartCoroutine(CallNPC());
    }

    public void CloseRestaurant()
    {
        GameManager.Instance.IsOpen = false;
        callVillageNPC = false;
        if(_callNPC != null)
        {
            StopCoroutine(_callNPC);
        }
    }

    private VillageGuest EnterNPC()
    {
        foreach (var npc in VillgeNPC)
        {
            if(npc.npcInfos.Holiday == GameData.Instance.Today)
            {
                return npc;
            }
        }
        return null;
    }

    private IEnumerator CallNPC()
    {
        while(GameManager.Instance.IsOpen)
        {
            AvailableChair();
            ChangeCallTime();
            yield return new WaitForSeconds(Random.Range(CallTime - 4, CallTime + 4));
            if (WaitChair.Count != 0)
            {
                if (callVillageNPC)
                {
                    if (EnterNPC() != null  && !EnterNPC().RestaurantVisit)
                    {
                        EnterNPC().gameObject.SetActive(true);
                        EnterNPC().RestaurantVisit = true;
                    }
                }
                GetObject();
            }
        }
    }

    private void AvailableChair()
    {
        WaitChair = GameObject.FindGameObjectsWithTag("Chair").ToList();
        WaitChair = WaitChair.Except(UseChair).ToList();
    }

    private void ChangeCallTime()
    {
        if (FirstNPC && GameData.Instance.GuestCount == 0)
        {
            CallTime = FirstCallTime;
            FirstNPC = false;
        }
        else
        {
            if (GameData.Instance.Fame >= 0)
            {
                CallTime = DefaultCallTime - GameData.Instance.Fame / 100;
            }
        }
    }

    public void AddObserver(IGameDataOb o)
    {
        o.AddObserver(this);
    }
    public void Change(GameData obj)
    {
        if (obj is GameData)
        {
            var GameData = obj;
            if(obj.TimeOfDay >= 1320)
            {
                CloseRestaurant();
            }
            if(GameManager.Instance.IsOpen)
            {
                callVillageNPC = (GameData.TimeOfDay >= VillageNPCTime) ? true : false;
            }
        }
    }
}
