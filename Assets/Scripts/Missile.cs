using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Missile : MonoBehaviour {
    [SerializeField] private float speed;

    [SerializeField] private GameObject playerHealthObject;
    [SerializeField] private Health playerHealthScript;

    [SerializeField] private AudioClip arrowStick;
    [SerializeField] private AudioClip arrowStart;

    private AudioSource audioSource;
    private AudioManager aM;
    private bool stuckInWall = false;

    private void Start() {
        aM = AudioManager.Instance;
        arrowStart = aM.sfxArrowStart;
        arrowStick = aM.sfxArrowStick;

        playerHealthObject = GameObject.Find("Player");
        playerHealthScript = playerHealthObject.GetComponent<Health>();

        audioSource = GetComponent<AudioSource>();
        audioSource.PlayOneShot(arrowStart, 0.8f);

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
            audioSource.PlayOneShot(arrowStick, 0.4f);
        }

        if (other.gameObject.tag == "Player") {
            if (stuckInWall == false) {
                Destroy(gameObject);
                IDamageable damageable = other.GetComponent<IDamageable>();
                if (damageable != null)
                {
                    damageable.TakeDamage(25, transform.position);
                }
            }


        }

        // else
        // {
        //     Destroy(gameObject);
        // }

    }
}