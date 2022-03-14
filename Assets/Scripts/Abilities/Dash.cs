using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class Dash : MonoBehaviour
{
    private PlayerController playerController;
    private GameObject player;

    public void OnDash()
    {
        player = GameObject.Find("Player");
        playerController = player.GetComponent<PlayerController>();

        playerController.baseSpeed *= 5;

    }

    public void End()
    {
        playerController.baseSpeed /= 5;
    }
}
