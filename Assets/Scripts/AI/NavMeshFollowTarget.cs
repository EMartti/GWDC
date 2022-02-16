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

    private bool goalDead = false;

    void Start() {
        agent = GetComponent<NavMeshAgent>();

        // Ettii pelaajan automaattisesti skenestä - Joona
        goal = GameObject.FindWithTag("Player").transform;

        Health.OnDeath += Health_OnDeath;
    }

    private void Update()
    {
        if (canSeeGoal == true)
        {
            lookAt();
        }
    }


    void lookAt()
    {
        transform.LookAt(goal);
    }

    private void Health_OnDeath(Health sender)
    {
        if (sender.gameObject == goal.gameObject) goalDead = true;
    }

    void FixedUpdate() {
        Vector3 offset = new Vector3(0, heightOffset, 0);

        distanceToTarget = Vector3.Distance(transform.position, goal.position);

        RaycastHit hit;
        Ray visionRay = new Ray(transform.position + offset, goal.transform.position - transform.position + offset);
        Debug.DrawRay(transform.position + offset, goal.transform.position - transform.position + offset, Color.red);

        if (!goalDead)
        {
            if (Physics.Raycast(visionRay, out hit))
            {
                if (hit.collider.tag == "Wall")
                {
                    canSeeGoal = false;
                }
                else
                {
                    canSeeGoal = true;
                }
            }
            if (distanceToTarget <= attackDist)
            {
                if (agent.isStopped == false)
                {
                    agent.isStopped = true; //stop the agent
                }

            }
            else
            {
                if (canSeeGoal == true)
                {
                    if (agent.isStopped == true)
                    {
                        agent.isStopped = false; //start moving again
                    }

                    agent.destination = goal.position;
                }
            }
        }
        else { canSeeGoal = false; agent.isStopped = true; }

    }

    private void OnDestroy()
    {
        Health.OnDeath -= Health_OnDeath;
    }
}
