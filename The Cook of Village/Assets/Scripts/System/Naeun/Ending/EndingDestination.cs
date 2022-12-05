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
            Controller.FatherAppearance(0); //아빠등장
            this.gameObject.SetActive(false);
        }
    }
}
