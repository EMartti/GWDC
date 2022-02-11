using StarterAssets;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

[RequireComponent(typeof(Health))]
public class DeathBehaviour : MonoBehaviour
{
    private int animIDIsDead;
    private Animator animator;
    private AudioSource audioSource;


    private PlayerInput starterInputActions;
    private PlayerInputActions playerInputActions;

    [SerializeField] private AudioClip deathSound;

    private void Awake()
    {        
        starterInputActions = GetComponent<PlayerInput>();
    }

    // Start is called before the first frame update
    void Start()
    {
        playerInputActions = PlayerInputs.Instance.playerInputActions;
        animator = GetComponent<Animator>();
        
        audioSource = GetComponent<AudioSource>();

        Health.OnDeath += Health_OnDeath;


        animIDIsDead = Animator.StringToHash("isDead");
    }

    public void Health_OnDeath(Health sender)
    {
        if(sender.gameObject == gameObject)
        {
            if(animIDIsDead != 0)
            {
                animator.SetBool(animIDIsDead, true);
                Invoke("SetIDfalse", 0.2f);
            }               

            if (deathSound != null)
                audioSource.PlayOneShot(deathSound, 0.7F);

            if (playerInputActions != null)
                playerInputActions.Player.Disable();

            if (starterInputActions != null)
                starterInputActions.enabled = false;

        }
        

    }

    private void SetIDfalse()
    {
        animator.SetBool(animIDIsDead, false);
    }

}
