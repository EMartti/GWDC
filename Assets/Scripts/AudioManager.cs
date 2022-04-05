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
    public AudioClip sfxAlert;
    public AudioClip sfxHurt;
    public AudioClip sfxHeal;
    public AudioClip sfxDie_P;
    public AudioClip sfxDie_E;
    public AudioClip sfxPickup;
    [Header("Combat")]
    public AudioClip sfxMeleeHit;
    public AudioClip sfxMeleeAttack;
    [Header("Projectile")]
    public AudioClip sfxArrowStart;
    public AudioClip sfxArrowStick;
    public AudioClip sfxFireballStart;
    public AudioClip sfxFireballExplode;
}
