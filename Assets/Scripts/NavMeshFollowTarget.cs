using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class NavMeshFollowTarget : MonoBehaviour {
    // Start is called before the first frame update
    public Transform goal;
    public NavMeshAgent agent;

    public float distanceToTarget;
    public float attackDist;
    public bool canSeeGoal = false;

    void Start() {
        agent = GetComponent<NavMeshAgent>();
    }

    // Update is called once per frame
    void FixedUpdate() {
        distanceToTarget = Vector3.Distance(transform.position, goal.position);

        RaycastHit hit;
        Ray visionRay = new Ray(transform.position, goal.transform.position - transform.position);
        Debug.DrawRay(transform.position, goal.transform.position - transform.position, Color.red);

        if (Physics.Raycast(visionRay, out hit, Mathf.Infinity)) {
            if (hit.collider.tag == "Wall") {
                canSeeGoal = false;print("where he go?");
            }
            if (hit.collider.tag == "Player") {
                canSeeGoal = true;print("ICU!");
            }
        }
        if (distanceToTarget <= attackDist) {
            agent.destination = transform.position; //stop the agent
        } else {
            if (canSeeGoal == true) {
                agent.destination = goal.position;
            }
        }
    }
}
