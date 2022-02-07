using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Spawner : MonoBehaviour
{
    public enum TypeToSpawn {Enemy, Boss }

    [SerializeField] private TypeToSpawn type;
    [SerializeField] private bool specific;



    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
