using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class NPCPooling : ObjectPooling<GuestNPC>
{
    [SerializeField]
    private GameObject ChairContainer;
    [SerializeField] private List<VillageGuest> VillgeNPC = new List<VillageGuest>();
    public List<GameObject> WaitChair = new List<GameObject>();

    [SerializeField]
    private float CallTime = 10f;
    private bool isCall = true;
    private Coroutine _callNPC;

    private void Start()
    {
        WaitChair = GameObject.FindGameObjectsWithTag("Chair").ToList();
    }

    public void OpenRestaurant()
    {
        _callNPC = StartCoroutine(CallNPC());
    }

    public void CloseRestaurant()
    {
        StopCoroutine(_callNPC);
    }

    private IEnumerator CallNPC()
    {
        while(isCall)
        {
            if (WaitChair.Count != 0)
            {
                GetObject();
            }
            yield return new WaitForSeconds(Random.Range(CallTime-4, CallTime+4));
        }
    }
}
