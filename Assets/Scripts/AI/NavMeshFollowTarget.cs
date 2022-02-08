using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class NavMeshFollowTarget : MonoBehaviour {
    // Start is called before the first frame update
    [SerializeField] private Transform goal;
    [SerializeField] private float heightOffset = 0.5f;
    public NavMeshAgent agent;

    public float distanceToTarget;
    public float attackDist;
    public bool canSeeGoal = false;

    void Start() {
        agent = GetComponent<NavMeshAgent>();

        // Ettii pelaajan automaattisesti skenest� - Joona
        goal = GameObject.Find("PlayerArmature").transform;
    }

    // Update is called once per frame
    void FixedUpdate() {
        Vector3 offset = new Vector3(0, heightOffset, 0);

        distanceToTarget = Vector3.Distance(transform.position, goal.position);

        RaycastHit hit;
        Ray visionRay = new Ray(transform.position + offset, goal.transform.position - transform.position + offset);
        Debug.DrawRay(transform.position + offset, goal.transform.position - transform.position + offset, Color.red);

        if (Physics.Raycast(visionRay, out hit)) {
            if (hit.collider.tag == "Wall") {
                canSeeGoal = false;
            }
            // - Joona - Muutin collider checki� ett� viholliset l�yt�� pelaajan oikein
            // Sein�t pit�� nyt tagata "Wall"-tagilla ett� skripti toimii oikein
            // Voi parannella my�hemmin
            else {
                canSeeGoal = true;
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
