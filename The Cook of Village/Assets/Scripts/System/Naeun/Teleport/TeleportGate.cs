using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TeleportGate : MonoBehaviour
{
    [SerializeField]public VillageTeleport.Gate gate;
    public VillageTeleport villgaeTeleprt;
    private void OnTriggerEnter(Collider other)
    {
        if(other.tag == "Player")
        {
            villgaeTeleprt.MoveGate(gate);
        }    
    }
}
