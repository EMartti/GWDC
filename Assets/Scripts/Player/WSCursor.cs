using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class WSCursor : MonoBehaviour
{
    [SerializeField] private GameObject cursorObj;
    [SerializeField] private Camera cam;
    [SerializeField] private LayerMask IgnoreMe;
    private bool cursorMovedOut;
    
    

    private void Start()
    {
        cam = GameObject.FindGameObjectWithTag("MainCamera").GetComponent<Camera>();
    }

    private void Update()
    {
        Ray ray = cam.ScreenPointToRay(Mouse.current.position.ReadValue());

        RaycastHit hit;

        if (Physics.Raycast(ray, out hit, 1000f, ~IgnoreMe))
        {
            // Ground check
            // && hit.collider.tag == "Ground"
            cursorObj.transform.position = hit.point;
        }                 
    }
}
