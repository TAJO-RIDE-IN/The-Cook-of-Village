using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TeleportGate : MonoBehaviour
{
    [SerializeField]public VillageTeleport.Gate gate;
    public VillageTeleport villgaeTeleprt;
    public float CameraX;
    public bool isOut;
    private void OnTriggerEnter(Collider other)
    {
        if(other.tag.Equals("Player"))
        {
            villgaeTeleprt.MoveGate(gate, isOut,CameraX);
        }    
    }
}
