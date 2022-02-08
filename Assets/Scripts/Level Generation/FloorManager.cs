using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;
using Unity.AI.Navigation;

public class FloorManager : MonoBehaviour
{
    [SerializeField] private Transform worldGeometry;
    [SerializeField] private GameObject player;

    private NavMeshSurface surface;


    private void Awake()
    {
        if(worldGeometry == null || worldGeometry.GetComponent<NavMeshSurface>() == null)
        {
            Debug.Log("World geometry of Floor Manager must be assigned and must have at least one NavMeshSurface");
            gameObject.SetActive(false);
            return;
        }

        surface = worldGeometry.GetComponentInChildren<NavMeshSurface>();
    }

    private void Start()
    {
        surface.BuildNavMesh();
    }
}
