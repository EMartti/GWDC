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
    private LayerMask planeMask;
    private bool cursorMovedOut;

    private Plane plane;
    private Player player;

    private void Start()
    {
        cam = GameObject.FindGameObjectWithTag("MainCamera").GetComponent<Camera>();
        player = Player.Instance;
    }

    private void LateUpdate()
    {
        plane = new Plane(Vector3.up, player.gameObject.transform.position);
        Ray ray = cam.ScreenPointToRay(Mouse.current.position.ReadValue());

        RaycastHit hit;
        float enter = 0f;

        //first cast to plane on player y axis
        if (plane.Raycast(ray, out enter))
        {
            Vector3 hitPoint = ray.GetPoint(enter);

            cursorObj.transform.position = hitPoint;
        }


        if (Physics.Raycast(ray, out hit, 1000f, ~IgnoreMe & ~IgnoreMe2))
        {
            // Ground check
            // && hit.collider.tag == "Ground"
            cursorObj.transform.position = hit.point;
        }
        
    }
}
