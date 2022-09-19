using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TeleportGate : MonoBehaviour
{
    public VillageTeleport villgaeTeleprt;
    private void OnTriggerEnter(Collider other)
    {
        if(other.tag == "Player")
        {
            villgaeTeleprt.MoveGate(this);
        }    
    }
}
