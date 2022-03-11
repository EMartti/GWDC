using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LootSpawn : MonoBehaviour {
    private float dropRandomizer;
    private GameObject healthPack;

    [SerializeField] private GameObject perkPoint;
    [SerializeField] private GameObject expOrb;
    [SerializeField] private bool willDropHP = false;

    [Range(0.0f, 1.0f)] //inspector slider for the float below
    [SerializeField] private float healthPackDropChance = 0.5f;
    [Range(0, 20)] //inspector slider for the float below
    [SerializeField] private int goldAmtToDrop = 10;
    [Range(0, 10)] //inspector slider for the float below
    [SerializeField] private int expAmtToDrop = 5;

    void Start() {
        for (int i = 0; i < goldAmtToDrop; i++) {
            Instantiate(perkPoint, new Vector3(transform.position.x, transform.position.y + 1, transform.position.z), Random.rotation, gameObject.transform);
        }

        for (int i = 0; i < expAmtToDrop; i++) {
            Instantiate(expOrb, new Vector3(transform.position.x, transform.position.y + 1, transform.position.z), Random.rotation, gameObject.transform);
        }

        healthPack = GetComponentInChildren<HealthPack>().gameObject;

        dropRandomizer = Random.Range(0, 2);
        if (dropRandomizer <= healthPackDropChance) {
            willDropHP = true;

        } else {
            willDropHP = false;
        }
        if (willDropHP == false) {
            Destroy(healthPack);
        }
    }
    private void Update() {
        if (healthPack = null) {
            Destroy(gameObject);
        }
    }
}

