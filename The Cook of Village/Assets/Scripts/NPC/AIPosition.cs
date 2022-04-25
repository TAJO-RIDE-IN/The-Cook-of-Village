using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.AI;

public class AIPosition : MonoBehaviour
{
    public List<GameObject> Chair = new List<GameObject>();

    private void Awake()
    {
        Chair = GameObject.FindGameObjectsWithTag("Chair").ToList();

    }

    private int ChairNum()
    {
        return Random.Range(0, Chair.Count);
    }
}
