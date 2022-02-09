using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Missile : MonoBehaviour {
    [SerializeField] private float speed;
    
    [SerializeField] private GameObject playerHealthObject;
    [SerializeField] private Health playerHealthScript;


    private void Start()
    {
        playerHealthObject = GameObject.Find("Player");
        playerHealthScript = playerHealthObject.GetComponent<Health>();
    }

    void Update() {
        GetComponent<Rigidbody>().AddForce(transform.forward * speed, ForceMode.Impulse);
        Destroy(gameObject, 10);
    }
    private void OnCollisionEnter(Collision collision) 
    {
        if (collision.gameObject.tag == "Wall") 
        {
            GetComponent<Rigidbody>().constraints = RigidbodyConstraints.FreezeAll;
        }

        if (collision.gameObject.tag == "Player")
        {
            Destroy(gameObject);
            playerHealthScript.TakeDamage(25);
        }

        // else
        // {
        //     Destroy(gameObject);
        // }
        
    }
}
