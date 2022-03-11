using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EmitParticles : MonoBehaviour
{
    [SerializeField] private GameObject particle;

    // Start is called before the first frame update
    void Start()
    {
        Instantiate(particle, transform.position, Quaternion.identity);
    }

    // Update is called once per frame
    void Update()
    {
        particle.transform.position = transform.position;
    }
}
