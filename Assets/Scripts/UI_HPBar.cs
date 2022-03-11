using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UI_HPBar : MonoBehaviour
{
    public Slider slider;
    public Health health;

    private void Start()
    {
        if(gameObject.tag == "Player")
            health = GameObject.Find("Player").GetComponent<Health>();
        slider = GetComponent<Slider>();
    }

    private void Update()
    {
        slider.maxValue = health.maxHealth;
        slider.value = health.currentHealth;
    }
}
