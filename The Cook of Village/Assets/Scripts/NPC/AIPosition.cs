using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.AI;

public interface IAIPosition
{
    Vector3 ChairPosition(List<GameObject> Object);
}

public class chairP:IAIPosition
{
    public Vector3 ChairPosition(List<GameObject> Object)
    {
        int chairNum = Random.Range(0, Object.Count);

        return (Object[chairNum].transform.position);
    }
}


public class AIPosition : MonoBehaviour
{
    public IAIPosition position { get; set; }
    public AIPosition(IAIPosition po)
    {
        position = po;
    }

    public List<GameObject> Chair = new List<GameObject>();
    public GameObject Counter;
    public GameObject Door;

    private void Awake()
    {
        Chair = GameObject.FindGameObjectsWithTag("Chair").ToList();
    }

    public Vector3 ChairPosition()
    {
        return position.ChairPosition(Chair);
    }
}
