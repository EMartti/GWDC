using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RandomDeleteOnStart : MonoBehaviour {
    [Range(0.0f, 1.0f)]
    public float spawnChance;
    void Start() {
        if (Random.value > spawnChance) {
            Destroy(gameObject);
        }
    }
}
