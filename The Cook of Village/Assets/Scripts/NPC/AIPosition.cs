using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class AIPosition : MonoBehaviour
{
    private List<GameObject> WaitChair = new List<GameObject>();
    public GameObject Counter;
    public GameObject Door;

    private void Awake()
    {
        WaitChair = GameObject.FindGameObjectsWithTag("Chair").ToList();
    }

    protected Vector3 ChairPosition()
    {

        int chairNum = Random.Range(0, WaitChair.Count);
        Vector3 destination = WaitChair[chairNum].transform.position;
        WaitChair.RemoveAt(chairNum);
        return (destination);
    }
}
