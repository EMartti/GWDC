using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Missile : MonoBehaviour {
    [SerializeField] private float speed;

    [SerializeField] private GameObject playerHealthObject;
    [SerializeField] private Health playerHealthScript;

    [SerializeField] private AudioClip arrowStick;

    private AudioSource audioSource;
    private bool stuckInWall = false;
    private void Start() {
        playerHealthObject = GameObject.Find("Player");
        playerHealthScript = playerHealthObject.GetComponent<Health>();

        audioSource = GetComponent<AudioSource>();

        GetComponent<Rigidbody>().AddForce(transform.forward * speed, ForceMode.Impulse);
        Destroy(gameObject, 10);
    }

    /*void Update() {
        GetComponent<Rigidbody>().AddForce(transform.forward * speed, ForceMode.Impulse);
       Destroy(gameObject, 10);
    }*/
    private void OnTriggerEnter(Collider other) {
        if (other.gameObject.tag == "Wall") {
            stuckInWall = true;
            GetComponent<Rigidbody>().constraints = RigidbodyConstraints.FreezeAll;
            audioSource.PlayOneShot(arrowStick, 0.5f);
        }

        if (other.gameObject.tag == "Player") {
            if (stuckInWall == false) {
                Destroy(gameObject);
                playerHealthScript.TakeDamage(25);
            }
        }

        // else
        // {
        //     Destroy(gameObject);
        // }

    }
}