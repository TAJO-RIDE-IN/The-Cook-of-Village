using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class NPCPooling : ObjectPooling<GuestNPC>
{
    [SerializeField]
    private GameObject ChairContainer;
    [SerializeField]
    private GameObject CounterLineContainer;

    public List<GameObject> WaitChair = new List<GameObject>();
    public List<GameObject> CounterWaitLine = new List<GameObject>();

    [SerializeField]
    private float CallTime = 10f;
    private bool isCall = true;

    private void Start()
    {
        WaitChair = GameObject.FindGameObjectsWithTag("Chair").ToList();
        //CounterWaitLine = ChairContainer.transform.GetComponentsInChildren<GameObject>().ToList();
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
