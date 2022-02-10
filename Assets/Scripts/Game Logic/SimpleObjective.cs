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

        }
    }

    private void Health_OnDeath(Health sender)
    {
        //OnObjectiveComplete?.Invoke(this, EventArgs.Empty);
    }
}

public enum ObjectiveType { Destroy, Else }
