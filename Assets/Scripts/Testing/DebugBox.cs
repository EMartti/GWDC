using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DebugBox : MonoBehaviour
{
    [SerializeField] private PlayerProgression pp;
    

    [SerializeField] private float xpAmountToGive;


    private void Start()
    {
        pp = GameObject.FindWithTag("Player").GetComponent<PlayerProgression>();
    }

    private void OnTriggerEnter(Collider other)
    {
        pp.GiveXp(xpAmountToGive);
    }
}
