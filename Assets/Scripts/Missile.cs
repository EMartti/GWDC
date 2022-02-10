using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Missile : MonoBehaviour {
    [SerializeField] private float speed;
    
    [SerializeField] private GameObject playerHealthObject;
    [SerializeField] private Health playerHealthScript;

    [SerializeField] private AudioClip arrowStick;
    private AudioSource audioSource;
    private void Start()
    {
        playerHealthObject = GameObject.Find("Player");
        playerHealthScript = playerHealthObject.GetComponent<Health>();

        audioSource = GetComponent<AudioSource>();
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
            audioSource.PlayOneShot(arrowStick, 0.5f);
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
