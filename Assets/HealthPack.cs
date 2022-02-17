using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HealthPack : MonoBehaviour {

    [SerializeField] private int healValue;
    private void OnTriggerEnter(Collider other) {
        if (other.CompareTag("Player")) {
            other.GetComponent<Health>().AddHealth(healValue);
            Destroy(gameObject);
        }
    }
}