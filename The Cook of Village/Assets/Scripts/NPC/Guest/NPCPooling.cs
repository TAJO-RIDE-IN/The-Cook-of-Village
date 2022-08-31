using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class NPCPooling : ObjectPooling<GuestNPC>, IObserver<GameData>
{
    [SerializeField] private List<VillageGuest> VillgeNPC = new List<VillageGuest>();
    public List<GameObject> WaitChair = new List<GameObject>();

    [SerializeField]
    private float CallTime = 10f;
    private bool open = false;
    private bool callVillageNPC = false;
    private bool NPCEnter = false;
    private Coroutine _callNPC;
    private float VillageNPCTime;
    private float OpenTime;

    private void Start()
    {
        AddObserver(GameData.Instance);
        WaitChair = GameObject.FindGameObjectsWithTag("Chair").ToList();
        OpenRestaurant();
    }

    public void OpenRestaurant()
    {
        open = true;
        OpenTime = GameData.Instance.TimeOfDay;
        VillageNPCTime = OpenTime + Random.Range(60, 181); // 마을 주민 오는시간 -> 오픈 후 1시간~3시간 사이
        _callNPC = StartCoroutine(CallNPC());
    }

    public void CloseRestaurant()
    {
        open = false;
        callVillageNPC = false;
        NPCEnter = false;
        StopCoroutine(_callNPC);
    }

    private VillageGuest EnterNPC()
    {
        foreach (var npc in VillgeNPC)
        {
            if(npc.Holiday == GameData.Instance.Today)
            {
                return npc;
            }
        }
        return null;
    }

    private IEnumerator CallNPC()
    {
        while(open)
        {
            if (WaitChair.Count != 0)
            {
                if (callVillageNPC && !NPCEnter) 
                {
                    if(EnterNPC() != null)
                    {
                        EnterNPC().gameObject.SetActive(true);
                        NPCEnter = true;
                    }
                }
                GetObject();
            }
            yield return new WaitForSeconds(Random.Range(CallTime-4, CallTime+4));
        }
    }
    private void OnDisable()
    {
        RemoveObserver(GameData.Instance);
    }

    public void AddObserver(IGameDataOb o)
    {
        o.AddObserver(this);
    }
    public void RemoveObserver(IGameDataOb o)
    {
        if(o != null) { o.RemoveObserver(this); }
    }

    public void Change(GameData obj)
    {
        if (obj is GameData)
        {
            var GameData = obj;
            if(open && !NPCEnter)
            {
                callVillageNPC = (GameData.TimeOfDay >= VillageNPCTime) ? true : false;
            }
        }
    }
}
