using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class NavMeshBaker : MonoBehaviour
{
    public NavMeshSurface navMeshSurfaces;
    private void Start()
    {
        navMeshSurfaces.BuildNavMesh();
    }
}
