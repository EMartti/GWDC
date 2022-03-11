using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class DestroyObjectives : Objective
{
    public GameObject[] objectives;
    public AudioClip completionSound;

    public TextMeshProUGUI objectivesList;

    void Awake()
    {
        foreach(GameObject objective in objectives)
        {

        }
    }

    public override bool IsAchieved()
    {
        return true;
    }

    public override void Complete()
    {

    }

    public override void DrawHUD()
    {
        objectivesList.SetText("");
    }


}
