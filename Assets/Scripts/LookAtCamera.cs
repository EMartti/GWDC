using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LookAtCamera : MonoBehaviour
{
    private GameObject camera;
    void Start()
    {
        camera = GameObject.Find("MainCamera");

    }

    // Update is called once per frame
    void Update()
    {
        transform.rotation = camera.transform.rotation;     
    }
}
