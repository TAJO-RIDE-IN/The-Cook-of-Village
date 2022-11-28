using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EndingDestination : MonoBehaviour
{
    public EndingController Controller;
    public void OnTriggerEnter(Collider other)
    {
        if (other.tag.Equals("Player"))
        {
            Controller.NextEvent();
            this.gameObject.SetActive(false);
        }
    }
}
