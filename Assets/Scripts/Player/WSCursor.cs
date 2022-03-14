using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class WSCursor : MonoBehaviour
{
    [SerializeField] private GameObject cursorObj;
    [SerializeField] private Camera cam;

    
    

    private void Start()
    {
        cam = GameObject.FindGameObjectWithTag("MainCamera").GetComponent<Camera>();
    }

    private void Update()
    {
        Ray ray = cam.ScreenPointToRay(Mouse.current.position.ReadValue());


        if (Physics.Raycast(ray, out RaycastHit raycastHit))
        {
            cursorObj.transform.position = raycastHit.point;
        }
    }
}
