using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerProgression : MonoBehaviour
{
    [SerializeField] private int playerLevel;
    [SerializeField] private float levelProgress = 0f;
    [SerializeField] private float expRequiredToLvlUp = 1000f;


    void LevelUp()
    {
        playerLevel += 1;
    }

    void EarnXp(float earnedXp)
    {
        levelProgress += earnedXp;
    }
}
