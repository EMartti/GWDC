using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

[RequireComponent(typeof(Health))]
public class DeathBehaviour : MonoBehaviour
{
    private int animIDIsDead;
    private Animator animator;
    private AudioSource audioSource;

    private Health health;

    [SerializeField] private AudioClip deathSound;

    // Start is called before the first frame update
    void Start()
    {
        animator = GetComponent<Animator>();

        audioSource = GetComponent<AudioSource>();


        animIDIsDead = Animator.StringToHash("isDead");
    }

    public void Health_OnDeath(Health sender)
    {
        if(animIDIsDead != 0)
        {
            animator.SetBool(animIDIsDead, true);
            Invoke("SetIDfalse", 0.2f);
        }       
        

        if (deathSound != null)
            audioSource.PlayOneShot(deathSound, 0.7F);
    }

    private void SetIDfalse()
    {
        animator.SetBool(animIDIsDead, false);
    }

}
