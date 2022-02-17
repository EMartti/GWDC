using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AudioManager : MonoBehaviour {

    #region Singleton
    public static AudioManager Instance
    {
        get
        {
            if (instance == null)
                instance = FindObjectOfType(typeof(AudioManager)) as AudioManager;

            return instance;
        }
        set
        {
            instance = value;
        }
    }
    private static AudioManager instance;
    #endregion

    [Header("Enemy")]
    public AudioClip sfxEnemyHurt;
    public AudioClip sfxEnemyHeal;
    public AudioClip sfxEnemyMeleeHit;
    [Header("Player")]
    public AudioClip sfxPlayerHurt;
    public AudioClip sfxPlayerHeal;
    public AudioClip sfxPlayerDie;
    public AudioClip sfxPlayerMeleeHit;
    public AudioClip sfxPlayerMeleeAttack;
    [Header("Projectile")]
    public AudioClip sfxArrowStart;
    public AudioClip sfxArrowStick;
    public AudioClip sfxFireballStart;
    public AudioClip sfxFireballExplode;
}
