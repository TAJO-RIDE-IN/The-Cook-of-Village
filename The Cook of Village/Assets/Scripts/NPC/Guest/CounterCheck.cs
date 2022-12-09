using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CounterCheck : MonoBehaviour
{
    public CounterQueue counter;

    public void OnTriggerStay(Collider other)
    {
        if(counter.waitngQueue.GuestQueue.Count > 0)
        {
            if (other.CompareTag("Guest"))
            {
                if(other.GetComponent<GuestNPC>().CurrentState.Equals(GuestNPC.State.Idle))
                {
                    if (counter.waitngQueue.GuestQueue.Peek().Equals(other.gameObject))
                    {
                        counter.CanPayNPC = other.gameObject;
                    }
                }
            }
        }
    }
}
