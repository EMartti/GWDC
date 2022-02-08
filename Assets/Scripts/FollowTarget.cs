using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[ExecuteInEditMode]
public class FollowTarget : MonoBehaviour
{
    // Muutin kamera-targetin väliaikasesti että sain pelaajan toimimaan, voi reverttaa ja kysyy multa -Joona 
    public GameObject target;
    [SerializeField] private float pivotHeight;
    [SerializeField] private float offset;
    [SerializeField] private float yaw;
    [Range(0f, 90f)][SerializeField] private float pitch;

    private float height; 


    // Start is called before the first frame update
    void Start()
    {
        // target = GameObject.FindWithTag("Player");
    }

    // Update is called once per frame
    void LateUpdate()
    {
        transform.rotation = Quaternion.Euler(Vector3.zero);
        transform.position = target.transform.position +  new Vector3(0f, GetHeight() + pivotHeight, -offset);
        transform.rotation = Quaternion.Euler(pitch, 0, 0);
        transform.RotateAround(target.transform.position, Vector3.up, yaw);
    }

    private float GetHeight()
    {        
        if (pitch != 0) height = offset * Mathf.Tan(Mathf.Deg2Rad * pitch);
        else height = 0;
        return height;
    }
}
