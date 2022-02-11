using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LootSpawn : MonoBehaviour {
    [SerializeField] private GameObject experiencePoint;
    [SerializeField] private GameObject perkPoint;
    [SerializeField] private GameObject healthPack;

    [SerializeField] private int amountToSpawn;
    // Start is called before the first frame update
    void Start() {
        for (int i = 0; i < amountToSpawn; i++) {
            var position = new Vector3(Random.Range(-0.1f, 0.1f), 0, Random.Range(-0.1f, 0.1f));
            Instantiate(perkPoint, position, Quaternion.identity);
        }
    }
}
