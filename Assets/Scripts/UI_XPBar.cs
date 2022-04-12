using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UI_XPBar : MonoBehaviour
{
    [SerializeField] private Slider slider;

    private void Start()
    {
        slider = GetComponent<Slider>();
    }

    private void Update()
    {
        slider.maxValue = PlayerStats.Instance.ppRequiredToLvlUp;
        slider.value = PlayerStats.Instance.currentPp;
    }
}
