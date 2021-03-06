using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Melee))]
public class AI_melee : MonoBehaviour
{

    private Melee melee;

    [SerializeField] private float attackInterval = 1f;

  
    private float timerPassed;

    void Start()
    {
        melee = gameObject.GetComponent<Melee>();
        timerPassed = 0f;
    }


    void Update()
    {
        timerPassed += Time.deltaTime;

        if (timerPassed >= attackInterval)
        {
            //melee.HitEvent();
            timerPassed = 0f;
        }        
    }
}
