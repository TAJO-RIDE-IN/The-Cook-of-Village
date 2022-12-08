using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class NPCPooling : ObjectPooling<GuestNPC>, IObserver<GameData>
{
    [SerializeField] private List<VillageGuest> VillgeNPC = new List<VillageGuest>();
    public List<GameObject> WaitChair = new List<GameObject>();
    public List<GameObject> UseChair = new List<GameObject>();

    public GameObject openUI;
    public GameObject closeUI;
    private bool isOpenTime;
    private float CallTime = 20f;
    [SerializeField] private float DefaultCallTime = 20f;
    private float FirstCallTime = 4f;
    private bool FirstNPC = false;
    private bool callVillageNPC = false;
    private Coroutine _callNPC;
    private float VillageNPCTime;
    private float OpenTime;

    private GameData gameData;
    private GameManager gameManager;
    private SoundManager soundManager;
    private bool isWorking;

    private void Start()
    {
        soundManager = SoundManager.Instance;
        gameData = GameData.Instance;
        gameManager = GameManager.Instance;
        AddObserver(gameData);
    }

    private void Update()
    {
        //일시정지 상태가 아닐 때, isOpen에 따라
        if (Input.GetKeyDown(KeyCode.P))
        {
            if (isOpenTime)
            {
                if (gameManager.IsOpen)
                {
                    CloseRestaurant();
                    return;
                }
                else
                {
                    OpenRestaurant();
                }
            }
            
        }
    }

    private void OpenUI()
    {
        isWorking = true;
        soundManager.Play(SoundManager.Instance._audioClips["OpenRestaurant"]);
        openUI.transform.LeanMoveLocalY(0, 0.5f).setEaseOutExpo().setOnComplete(() => StartCoroutine(Wait(openUI)));
    }

    private IEnumerator Wait(GameObject gameObject)
    {
        yield return new WaitForSeconds(0.5f);
        gameObject.transform.LeanMoveLocalY(725, 0.5f).setEaseOutExpo();
        isWorking = false;
    }
    private void CloseUI()
    {
        isWorking = true;
        soundManager.Play(SoundManager.Instance._audioClips["CloseRestaurant"]);
        closeUI.transform.LeanMoveLocalY(0, 0.5f).setEaseOutExpo().setOnComplete(() => StartCoroutine(Wait(closeUI)));
    }


    public void OpenRestaurant()
    {
        if (!isWorking)
        {
            OpenUI();
            gameManager.IsOpen = true;
            FirstNPC = true;
            OpenTime = GameData.Instance.TimeOfDay;
            VillageNPCTime = OpenTime + Random.Range(60, 181); // 마을 주민 오는시간 -> 오픈 후 1시간~3시간 사이
        
            if(_callNPC != null)
            {
                StopCoroutine(_callNPC);
            }
            _callNPC = StartCoroutine(CallNPC());
        }
        
    }

    public void CloseRestaurant()
    {
        if (!isWorking)
        {
            CloseUI();
            gameManager.IsOpen = false;
            callVillageNPC = false;
            if(_callNPC != null)
            {
                StopCoroutine(_callNPC);
            }
        }       
    }
    public void ExitGuestNPC()
    {

    }
    private VillageGuest EnterNPC()
    {
        foreach (var npc in VillgeNPC)
        {
            if(npc.npcInfos.Holiday.Equals(GameData.Instance.Today))
            {
                return npc;
            }
        }
        return null;
    }

    private IEnumerator CallNPC()
    {
        while(gameManager.IsOpen)
        {
            AvailableChair();
            ChangeCallTime();
            yield return new WaitForSeconds(Random.Range(CallTime - 4, CallTime + 4));
            if (WaitChair.Count != 0)
            {
                if (callVillageNPC)
                {
                    VillageGuest village = EnterNPC();
                    if (village != null  && !village.npcInfos.VisitRestaurant)
                    {
                        village.gameObject.SetActive(true);
                        village.VisitRestaurant();
                        callVillageNPC = false;
                        yield return null;
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
        if (FirstNPC && gameData.GuestCount == 0)
        {
            CallTime = FirstCallTime;
            FirstNPC = false;
        }
        else
        {
            if (gameData.Fame >= 0)
            {
                CallTime = DefaultCallTime - gameData.Fame / 100;
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
            if(obj.TimeOfDay >= 1320 || obj.TimeOfDay <= 120)
            {
                if(gameManager.IsOpen)
                {
                    CloseRestaurant();
                }
                isOpenTime = false;
            }
            else
            {
                isOpenTime = true;
            }
            if(gameManager.IsOpen)
            {
                callVillageNPC = (GameData.TimeOfDay >= VillageNPCTime) ? true : false;
            }
        }
    }
}
