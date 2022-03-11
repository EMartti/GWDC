using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
public class Door : MonoBehaviour
{
    [SerializeField] private float timeToMove = 1f;
    [SerializeField] private Vector3 offset = Vector3.zero;
    // Update is called once per frame

    [SerializeField] private GameObject objectiveGO;
    private Vector3 targetPos;

    void Start()
    {
        SimpleObjective objective = objectiveGO.GetComponent<SimpleObjective>();
        objective.OnObjectiveComplete += Objective_OnCompletion;
        targetPos = transform.position + offset;
    }

    private void Objective_OnCompletion(object sender, EventArgs e)
    {
        StartCoroutine(LerpFunction(targetPos, timeToMove));
    }

    IEnumerator LerpFunction(Vector3 targetPos, float duration)
    {
        float time = 0;
        Vector3 startPos = transform.position;
        while (time < duration)
        {
            transform.position = Vector3.Lerp(startPos, targetPos, time / duration);
            time += Time.deltaTime;
            yield return null;
        }
        transform.position = targetPos;
    }
}
