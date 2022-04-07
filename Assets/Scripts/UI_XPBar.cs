using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UI_XPBar : MonoBehaviour
{
    [SerializeField] private Slider slider;

    private void Start()
    {
        int points = Player.Instance.playerPerks.GetPerkPoints();
        slider = GetComponent<Slider>();
    }

    private void Update()
    {
        slider.maxValue = PlayerStats.Instance.xpRequiredToLvlUp;
        slider.value = PlayerStats.Instance.currentXp;
    }

    //
}
