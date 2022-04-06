using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SelectThisWeapon : MonoBehaviour
{
    [SerializeField] private SelectWeapon selectWeapon;
    [SerializeField] private weaponType thisWeapon;

    private void OnTriggerEnter(Collider other)
    {
        if(other.tag == "Player")
        {
            selectWeapon.SwitchWeapon(thisWeapon);
        }
    }
}
