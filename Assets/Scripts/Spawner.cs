using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Spawner : MonoBehaviour
{
    public enum TypeToSpawn {Enemy, Boss }

    [SerializeField] private GameObject[] gameObjects;
    [SerializeField] private bool specific;

    [SerializeField] private GameObject specificGameObject;


    // Start is called before the first frame update
    void Start()
    {
        Spawn();
    }

    private void Spawn()
    {
        if (specific) Instantiate(specificGameObject, transform.position, Quaternion.identity);
        else
        {
            int rand = Random.Range(0, gameObjects.Length);
            Instantiate(gameObjects[rand], transform.position, Quaternion.identity);
        }
       
    }
}
