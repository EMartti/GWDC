using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WSCursor : MonoBehaviour
{
    [SerializeField] private GameObject cursorObj;
    [SerializeField] private Camera cam;
    [SerializeField] private Vector3 cursorPos;


    private void Start()
    {
        cam = GameObject.FindWithTag("MainCamera").GetComponent<Camera>();

        
        cursorPos = cam.ScreenToWorldPoint(Input.mousePosition);
        cursorObj.transform.position = cursorPos;
        Instantiate(cursorObj, cursorPos, Quaternion.identity);
    }

    void Update()
    {
        // cursorPos = cam.ScreenToWorldPoint(Input.mousePosition);
        // cursorObj.transform.position = 
    }
}
