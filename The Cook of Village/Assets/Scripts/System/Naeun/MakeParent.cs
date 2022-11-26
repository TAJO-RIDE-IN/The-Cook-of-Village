using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MakeParent : MonoBehaviour
{
    public List<GameObject> Object3D = new List<GameObject>();
    public GameObject EmptyObject;
    private void Start()
    {
        foreach(var obj in Object3D)
        {
            GameObject parentObject;
            parentObject = Instantiate(EmptyObject);
            obj.AddComponent<MeshCollider>();
            obj.GetComponent<MeshCollider>().convex = true;
            parentObject.name = obj.name;
            obj.transform.position = Vector3.zero;
            obj.transform.parent = parentObject.transform;
        }
    }
}
