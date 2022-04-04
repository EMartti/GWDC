using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerProgression : MonoBehaviour
{
    public event EventHandler OnLevelUp;

    [SerializeField] private PlayerStats playerStats;
    [SerializeField] private Health playerHealthScript;
    [SerializeField] private Melee meleeScript;

    [Header("Level & XP")]
    [SerializeField] private float xpRequiredMultiplier = 1.5f;
    [SerializeField] private int playerStartLevel;

    [Header("Health Settings")]
    [SerializeField] private int maxHealthAddedPerLevel;

    [Header("Damage")]
    [SerializeField] private int damageAddedPerLevel = 10;

    [Serializable]
    public class AudioInspector {
        public AudioClip giveExpSound;        
    }
    
    private AudioManager aM;
    AudioSource audioSource;
    [SerializeField] private AudioInspector myAudio;

    private void Start()
    {
        audioSource = GetComponent<AudioSource>();
        aM = AudioManager.Instance;

        if (myAudio.giveExpSound == null) {
            myAudio.giveExpSound = aM.sfxPickup;
        }
        playerStats = PlayerStats.Instance;
        playerHealthScript = GameObject.FindWithTag("Player").GetComponent<Health>();
        meleeScript = GameObject.FindWithTag("Player").GetComponent<Melee>();
    }

    public void GiveXp(float earnedXp)
    {
        if (myAudio.giveExpSound != null)
            audioSource.PlayOneShot(myAudio.giveExpSound, 2f);

        playerStats.currentXp += earnedXp;

        // Debug
        //Debug.Log("Player earned " + earnedXp.ToString() + "XP");

        if (playerStats.currentXp >= playerStats.xpRequiredToLvlUp)
        { 
            LevelUp();
        }
    }

    public void LevelUp()
    {
        // Remove level xp from currentXp
        playerStats.currentXp -= playerStats.xpRequiredToLvlUp;

        // Level / stat increase
        playerStats.playerLevel += 1;
        playerStats.xpRequiredToLvlUp *= xpRequiredMultiplier;
            
        // Health increase
        playerStats.healthBonus += maxHealthAddedPerLevel;
        playerHealthScript.maxHealth = playerHealthScript.defaultHealth + playerStats.healthBonus;
        playerHealthScript.currentHealth = playerHealthScript.maxHealth;
        
        // Damage increase
        playerStats.damageBonus += damageAddedPerLevel;
        meleeScript.parameters.hitDamage = meleeScript.parameters.baseDamage + PlayerStats.Instance.damageBonus;

        // Cast event
        if (OnLevelUp != null) OnLevelUp.Invoke(this, EventArgs.Empty);

        // Debug
        Debug.Log("Player achieved Level " + playerStats.playerLevel + "!");


        // Jos edellisestä level-upista jäi ylimäärästä xp:tä, joka riittää toiseen leveliin - Level uppaa uudestaan
        if (playerStats.currentXp >= playerStats.xpRequiredToLvlUp)
        {
            LevelUp();
        }
    }
}
