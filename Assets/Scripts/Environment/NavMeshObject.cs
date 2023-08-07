using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class NavMeshObject : MonoBehaviour
{
    public NavMeshSurface surface;

    public void BuildNavMesh()
    {
        surface.BuildNavMesh();
    }

    public void DestroyNavMesh()
    {
        surface.RemoveData();
    }
}
