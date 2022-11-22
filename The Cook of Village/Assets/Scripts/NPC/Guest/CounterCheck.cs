using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CounterCheck : MonoBehaviour
{
    public CounterQueue counter;

    public void OnTriggerEnter(Collider other)
    {
        if(counter.waitngQueue.GuestQueue.Count > 0)
        {
            if (other.CompareTag("Guest"))
            {
                if (counter.waitngQueue.GuestQueue.Peek().Equals(other.gameObject))
                {
                    counter.CanPayNPC = other.gameObject;
                }
            }
        }
    }
}
