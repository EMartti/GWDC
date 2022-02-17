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

    [Header("Status")]
    public AudioClip sfxHurt;
    public AudioClip sfxHeal;
    [Header("Player")]
    public AudioClip sfxPlayerHurt;
    public AudioClip sfxPlayerHeal;
    public AudioClip sfxPlayerDie;
    public AudioClip sfxMeleeHit;
    public AudioClip sfxMeleeAttack;
    [Header("Projectile")]
    public AudioClip sfxArrowStart;
    public AudioClip sfxArrowStick;
    public AudioClip sfxFireballStart;
    public AudioClip sfxFireballExplode;
}
