using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
using UnityEngine.InputSystem;

public class MouseTargeting : MonoBehaviour
{
    private Plane plane;
    private float distance;
    Ray ray;

    private PlayerInputActions playerInputActions;

    private void Awake()
    {
        playerInputActions = new PlayerInputActions();
    }

    private void Start()
    {
        ray = Camera.main.ScreenPointToRay(Mouse.current.position.ReadValue());
        plane = new Plane(Vector3.up, Vector3.zero);

        playerInputActions.Player.MousePosition.Enable();
    }

    private void Update()
    {
        ray = Camera.main.ScreenPointToRay(Mouse.current.position.ReadValue());
        if (plane.Raycast(ray, out distance)) 
        {
        Vector3 target = ray.GetPoint(distance);
        Vector3 direction = target - transform.position;
        float rotation = Mathf.Atan2(direction.x, direction.z) * Mathf.Rad2Deg;
        transform.rotation=Quaternion.Euler(0, rotation, 0);
        }     
    }    
}
