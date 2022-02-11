using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RangedAttack : MonoBehaviour {
    public GameObject projectile;
    public float attackInterval;
    public NavMeshFollowTarget moveScript;


    public Vector3 projectileStartOffset;

    public bool hasAttacked;
    void Start() {
        moveScript = GetComponent<NavMeshFollowTarget>();
    }

    void Update() {
        if (moveScript.canSeeGoal) {
            if (moveScript.distanceToTarget <= (moveScript.attackDist + 1)) {
                if (hasAttacked == false) {
                    hasAttacked = true;
                    Instantiate(projectile, transform.position + projectileStartOffset, transform.rotation); //shoot arrow
                    Invoke("Delay", attackInterval);
                }
            }
        }
    }

    private void Delay() {
        hasAttacked = false;
    }
}
