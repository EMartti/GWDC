using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerProgression : MonoBehaviour {
    public event EventHandler OnXpLevelUp;
    public event EventHandler OnPpLevelUp;

    private Player player;
    [SerializeField] private PlayerStats playerStats;
    [SerializeField] private Health playerHealthScript;

    [Header("Level & XP % Pp")]
    [SerializeField] private float xpRequiredMultiplier = 1.5f;
    [SerializeField] private int playerStartLevel;
 
    [Header("Health Settings")]
    [SerializeField] private int maxHealthAddedPerLevel;

    [Header("Damage")]
    [SerializeField] private int damageAddedPerLevel = 10;

    [Serializable]
    public class AudioInspector {
        public AudioClip giveExpSound;
        public AudioClip givePPSound;
    }

    private AudioManager aM;
    AudioSource audioSource;
    [SerializeField] private AudioInspector myAudio;

    private void Start() {
        audioSource = GetComponent<AudioSource>();
        aM = AudioManager.Instance;

        if (myAudio.giveExpSound == null) {
            myAudio.giveExpSound = aM.sfxExpPickup;
        }
        if (myAudio.givePPSound == null) {
            myAudio.givePPSound = aM.sfxPPPickup;
        }

        playerStats = PlayerStats.Instance;
        playerHealthScript = GameObject.FindWithTag("Player").GetComponent<Health>();

        player = Player.Instance;
    }

    // Give Xp to player
    public void GiveXp(float earnedXp) {
        if (myAudio.giveExpSound != null)
            audioSource.PlayOneShot(myAudio.giveExpSound, 2f);

        playerStats.currentXp += earnedXp;

        if (playerStats.currentXp >= playerStats.xpRequiredToLvlUp) {
            MetaLevelUp();
        }
    }

    // Give PerkPoints to player
    public void GivePP(float earnedPp) {
        if (myAudio.givePPSound != null)
            audioSource.PlayOneShot(myAudio.givePPSound, 2f);

        playerStats.currentPp += earnedPp;

        if (playerStats.currentPp >= playerStats.ppRequiredToLvlUp)
        {
            PerkLevelUp();
        }
    }

    public void MetaLevelUp() {
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
        player.damageBonus = PlayerStats.Instance.damageBonus;

        // Cast event
        if (OnXpLevelUp != null) OnXpLevelUp.Invoke(this, EventArgs.Empty);

        // Debug
        Debug.Log("Player achieved Level " + playerStats.playerLevel + "!");

        // Vaihda UI:n playerLevel teksti  
        GameManager.Instance.GetComponentInChildren<UIPerks>().UpdateMetaLevelText();
        
        // Jos edellisestä level-upista jäi ylimäärästä xp:tä, joka riittää toiseen leveliin - Level uppaa uudestaan
        if (playerStats.currentXp >= playerStats.xpRequiredToLvlUp) {
            MetaLevelUp();
        }
    }

    public void PerkLevelUp()
    {
        playerStats.currentPp = 0f;

        //Cast event
        if (OnPpLevelUp != null) OnPpLevelUp.Invoke(this, EventArgs.Empty);

        // Debug
        Debug.Log("Player unlocked a perk point!");
    }
}
