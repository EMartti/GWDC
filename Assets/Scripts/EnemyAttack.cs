using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyAttack : MonoBehaviour {
    public GameObject projectile;
    public float attackInterval;
    public NavMeshFollowTarget moveScript;

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
        Instantiate(projectile, transform.position, transform.rotation);
        yield return new WaitForSeconds(attackInterval);
        hasAttacked = false;
    }
}
