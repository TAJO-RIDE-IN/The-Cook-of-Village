using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NPCPooling : ObjectPooling<RestaurantNPC>
{
    private bool isCall = true;
    [SerializeField]
    private float CallTime = 10f;
    private void Start()
    {
        StartCoroutine(CallNPC());
    }

    private IEnumerator CallNPC()
    {
        while(isCall)
        {
            GetObject();
            yield return new WaitForSeconds(Random.Range(CallTime-4, CallTime+4));
        }
    }
}
