using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class SimpleObjective : MonoBehaviour
{
    public event EventHandler OnObjectiveComplete;

    [SerializeField] private ObjectiveType objectiveType;

    void Start()
    {
        
        if(objectiveType == ObjectiveType.Destroy)
        {
            Health health = GetComponent<Health>();
            health.OnDeath += Health_OnDeath;
        }
    }

    private void Health_OnDeath(object sender, EventArgs e)
    {
        OnObjectiveComplete?.Invoke(this, EventArgs.Empty);
    }
}

public enum ObjectiveType { Destroy, Else }
