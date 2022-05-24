using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class NPCPooling : ObjectPooling<GuestNPC>
{
    [SerializeField]
    private GameObject ChairContainer;
    public List<GameObject> WaitChair = new List<GameObject>();

    [SerializeField]
    private float CallTime = 10f;
    private bool isCall = true;

    private void Start()
    {
        WaitChair = GameObject.FindGameObjectsWithTag("Chair").ToList();
        StartCoroutine(CallNPC());
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
