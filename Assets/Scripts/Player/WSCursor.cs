using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class WSCursor : MonoBehaviour
{
    [SerializeField] private GameObject cursorObj;
    [SerializeField] private Camera cam;
    [SerializeField] private LayerMask IgnoreMe;
    [SerializeField] private LayerMask IgnoreMe2;
    private bool cursorMovedOut;
    
    

    private void Start()
    {
        cam = GameObject.FindGameObjectWithTag("MainCamera").GetComponent<Camera>();
    }

    private void LateUpdate()
    {
        Ray ray = cam.ScreenPointToRay(Mouse.current.position.ReadValue());

        RaycastHit hit;

        if (Physics.Raycast(ray, out hit, 1000f, ~IgnoreMe & ~IgnoreMe2))
        {
            // Ground check
            // && hit.collider.tag == "Ground"
            cursorObj.transform.position = hit.point;
        }                 
    }
}
