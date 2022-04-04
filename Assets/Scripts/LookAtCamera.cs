using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LookAtCamera : MonoBehaviour
{
    private GameObject mainCamera;
    void Start()
    {
        mainCamera = GameObject.Find("MainCamera");

    }

    // Update is called once per frame
    void Update()
    {
        transform.rotation = mainCamera.transform.rotation;     
    }
}
