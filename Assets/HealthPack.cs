using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HealthPack : MonoBehaviour {

    [SerializeField] private int healValue;
    // Update is called once per frame
    private void OnTriggerEnter(Collider other) {
        if (other.tag == "Player") {
            other.GetComponent<Health>().AddHealth(healValue);
            Destroy(gameObject);
        }
    }
}
