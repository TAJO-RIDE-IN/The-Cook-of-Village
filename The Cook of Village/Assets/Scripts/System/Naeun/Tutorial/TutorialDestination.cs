using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TutorialDestination : MonoBehaviour
{
    public TutorialVillageController VillageController;
    public void OnTriggerEnter(Collider other)
    {
        if(other.tag.Equals("Player"))
        {
            VillageController.VillageTutorialUI.DialogueText();
            GetComponent<BoxCollider>().enabled = false;
            this.gameObject.SetActive(false);
        }
    }
}
