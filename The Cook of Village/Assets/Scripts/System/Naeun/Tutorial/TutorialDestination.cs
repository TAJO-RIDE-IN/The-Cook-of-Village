using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TutorialDestination : MonoBehaviour
{
    public TutorialController Controller;
    public void OnTriggerEnter(Collider other)
    {
        if(other.tag.Equals("Player"))
        {
            Controller.NextDialogue();
            GetComponent<BoxCollider>().enabled = false;
            this.gameObject.SetActive(false);
        }
    }
}
