using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AI_melee : MonoBehaviour
{

    private Melee melee;

    [SerializeField] private float attackInterval;

    private float distanceToPlayer;
    private float timerPassed;

    void Start()
    {
        melee = GetComponent<Melee>();
        timerPassed = 0f;
    }


    void Update()
    {
        timerPassed += Time.deltaTime;

        if (timerPassed >= attackInterval)
        {
            melee.HitEvent();
            timerPassed = 0f;
        }
        
    }
}
