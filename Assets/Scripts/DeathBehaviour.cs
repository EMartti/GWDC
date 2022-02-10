using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

[RequireComponent(typeof(Health))]
public class DeathBehaviour : MonoBehaviour
{
    private int animIDIsDead;
    private Animator animator;

    // Start is called before the first frame update
    void Start()
    {
        animator = GetComponent<Animator>();

        Health health = GetComponent<Health>();
        health.OnDeath += Health_OnDeath;

        animIDIsDead = Animator.StringToHash("isDead");
    }

    private void Health_OnDeath(object sender, EventArgs e)
    {
        animator.SetBool(animIDIsDead, true);
        Invoke("SetIDfalse", 0.2f);
    }

    private void SetIDfalse()
    {
        animator.SetBool(animIDIsDead, false);
    }

}
