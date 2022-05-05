using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class DebugButtons : MonoBehaviour
{
    [SerializeField] private Button damageButton;
    [SerializeField] private Button ppButton;
    [SerializeField] private Button xpButton;

    [SerializeField] private Player player;
    [SerializeField] private PlayerProgression playerProgression;
    [SerializeField] private Health playerHealth;

    [SerializeField] private int damageToGive = 10;
    [SerializeField] private float ppToGive = 1000f;
    [SerializeField] private float xpToGive = 1000f;



    private void Start()
    {
        player = Player.Instance;
        playerProgression = player.gameObject.GetComponentInChildren<PlayerProgression>();
        playerHealth = player.gameObject.GetComponentInChildren<Health>();

        damageButton.onClick.AddListener(OnDamageButtonClicked);
        ppButton.onClick.AddListener(OnPPButtonClicked);
        xpButton.onClick.AddListener(OnXPButtonClicked);
    }

    private void OnDamageButtonClicked()
    {
        playerHealth.TakeDamage2(damageToGive);
    }

    private void OnPPButtonClicked()
    {
        playerProgression.GivePP(ppToGive);
    }

    private void OnXPButtonClicked()
    {
        playerProgression.GiveXp(xpToGive);
    }

}
