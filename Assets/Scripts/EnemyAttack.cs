using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyAttack : MonoBehaviour {
    public GameObject projectile;
    public float attackInterval;
    public NavMeshFollowTarget moveScript;

    // - Joona - 
    // Lisäsin offsetin projektiilin lähtöpisteeseen, ettei nuoli kulje maan pintaa pitkin
    public Vector3 projectileStartOffset;

    public bool hasAttacked;
    void Start() {
        moveScript = GetComponent<NavMeshFollowTarget>();
    }

    void Update() {
        if (moveScript.canSeeGoal) {
            if (hasAttacked == false) {
                StartCoroutine("Attack");
            }
        }
    }
    IEnumerator Attack() {
        hasAttacked = true;
        Instantiate(projectile, transform.position + projectileStartOffset, transform.rotation);
        yield return new WaitForSeconds(attackInterval);
        hasAttacked = false;
    }
}
