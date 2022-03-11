using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Collectible : MonoBehaviour {
    [SerializeField] private GameObject collector;
    [SerializeField] private float speed;
    [SerializeField] private float pickUpDist;
    [SerializeField] private bool isExpOrb;
    [SerializeField] private float xpToGive = 100f;

    private bool closeEnoughToPickup = false;
    private float distToCollector;
    private Rigidbody rb;

    void Start() {
        rb = GetComponent<Rigidbody>();
        collector = GameObject.FindGameObjectWithTag("Player");
    }

    void Update() {
        distToCollector = Vector3.Distance(transform.position, collector.transform.position);

        if (closeEnoughToPickup == true) {
            transform.LookAt(collector.transform);
            rb.AddForce(transform.forward * speed, ForceMode.Force);
        }

        if (distToCollector < 0.5f) {
            PickUp();
        }

        if (distToCollector < pickUpDist) {
            closeEnoughToPickup = true;
        }
    }

    void PickUp() {
        if (isExpOrb == true) {
            collector.GetComponent<PlayerProgression>().GiveXp(xpToGive);
        }
        Destroy(gameObject);
    }
}
