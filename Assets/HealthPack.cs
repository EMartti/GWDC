using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HealthPack : MonoBehaviour {

    [SerializeField] private int healValue;
    [SerializeField] private float distanceToFloor;
    private void OnTriggerEnter(Collider other) {
        if (other.CompareTag("Player")) {
            other.GetComponent<Health>().AddHealth(healValue);
            Destroy(gameObject);
        }
    }
    private void Start() {
        RaycastHit hit;
        if (Physics.Raycast(transform.position, Vector3.down, out hit)) {            
            transform.position = hit.point;
        }
    }
}