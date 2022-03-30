using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Spawner : MonoBehaviour
{
    [SerializeField] private GameObject[] gameObjects;
    [Range(0, 10)] [SerializeField] private int quantityVariationRange;
    [Range(0f, 360f)] [SerializeField] private float randomRotationRange;

    // Start is called before the first frame update
    void Start()
    {
        int randVariation = Random.Range(0, quantityVariationRange);

        for (int i = 0; i < gameObjects.Length + Random.Range(0, randVariation); i++)
        {
            Spawn();
        }
    }

    private void Spawn()
    {
        int randObj = Random.Range(0, gameObjects.Length);
        Quaternion randRot = Quaternion.Euler(0f, Random.Range(0, randomRotationRange), 0f);

        Instantiate(gameObjects[randObj], transform.position, randRot);       
    }
}
