using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class NavMeshBaker : MonoBehaviour
{
    public static NavMeshSurface navMeshSurfaces;
    private void Awake()
    {
        navMeshSurfaces = this.GetComponent<NavMeshSurface>();
        NavMeshBake();
    }
    public static void NavMeshBake()
    {
        if(navMeshSurfaces != null)
        {
            navMeshSurfaces.BuildNavMesh();
        }
    }
}
