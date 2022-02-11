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
            Health.OnDeath += Health_OnDeath;
        }
    }

    private void Health_OnDeath(Health sender)
    {
        if (sender.gameObject == gameObject) 
        {
            if(OnObjectiveComplete != null) OnObjectiveComplete.Invoke(this, EventArgs.Empty);
        }
    }

    private void OnDestroy()
    {
        Health.OnDeath -= Health_OnDeath;
    }
}

public enum ObjectiveType { Destroy, Else }
