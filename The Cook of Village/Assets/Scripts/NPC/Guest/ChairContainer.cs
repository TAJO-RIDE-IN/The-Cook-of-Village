using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class ChairContainer : MonoBehaviour
{
    public List<GameObject> WaitChair = new List<GameObject>();
    private void Awake()
    {
        WaitChair = GameObject.FindGameObjectsWithTag("Chair").ToList();
    }
}
