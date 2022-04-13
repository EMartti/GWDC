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
        player = Player.Instance.gameObject;
        playerController = player.GetComponent<PlayerController>();

        playerController.baseSpeed *= 5;
        playerController.dashing = true;
    }

    public void EndDash()
    {
        playerController.baseSpeed /= 5;
        playerController.dashing = false;
    }
}
