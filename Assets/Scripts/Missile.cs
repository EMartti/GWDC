using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Missile : MonoBehaviour {
    [SerializeField] private float speed;
    void Update() {
        GetComponent<Rigidbody>().AddForce(transform.forward * speed, ForceMode.Impulse);
        Destroy(gameObject, 10);
    }
    private void OnCollisionEnter(Collision collision) {
            GetComponent<Rigidbody>().constraints = RigidbodyConstraints.FreezeAll;
        if (collision.gameObject.tag == "Player") {
            //TakeDamage
            Destroy(gameObject);
        }
    }
}
