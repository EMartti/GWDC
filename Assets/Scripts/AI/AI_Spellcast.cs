using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AI_Spellcast : MonoBehaviour
{
    private Weapon weaponScript;
    private NavMeshFollowTarget navMeshScript;


    
    void Start()
    {
        weaponScript = GetComponent<Weapon>();
        navMeshScript = GetComponent<NavMeshFollowTarget>();
    }


    void Update()
    {
        if (navMeshScript.canSeeGoal == true)
        {
            weaponScript.AiFire();
        }
    }
}
