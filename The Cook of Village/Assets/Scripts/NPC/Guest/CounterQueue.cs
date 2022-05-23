using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CounterQueue : MonoBehaviour
{
    private Queue<Vector3> WaittingPosition = new Queue<Vector3>();
    [SerializeField]
    private Transform[] ArrayLine;

    private void Start()
    {
        ArrayLine = this.gameObject.GetComponentsInChildren<Transform>();
        foreach(Transform line in ArrayLine)
        {
            WaittingPosition.Enqueue(line.position);
        }
    }

}
