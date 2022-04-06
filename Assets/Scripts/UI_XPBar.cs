using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UI_XPBar : MonoBehaviour
{
    public Slider slider;
    private PlayerStats stats;

    private Player player;

    private void Start()
    {
        int points = Player.Instance.playerPerks.GetPerkPoints();
        stats = GameObject.Find("PlayerStats").GetComponent<PlayerStats>();
        slider = GetComponent<Slider>();
    }

    private void Update()
    {
        slider.maxValue = stats.xpRequiredToLvlUp;
        slider.value = stats.currentXp;
    }
}
