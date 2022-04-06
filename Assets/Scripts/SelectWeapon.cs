using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SelectWeapon : MonoBehaviour
{
    private GameObject player;
    private GameObject rangeWeapon;
    private GameObject magicWeapon;


    [SerializeField] private GameObject sword;
    [SerializeField] private GameObject bow;
    [SerializeField] private GameObject hammer;

    private Melee melee;
    private Range range;
    private Magic magic;

    private GameManager gameManager;

    private enum WeaponType { Melee, Range, Magic }
    [SerializeField] private WeaponType weapon;



    // Start is called before the first frame update
    void Start()
    {
        gameManager = GameManager.Instance;

        player = GameObject.Find("Player");

        melee = player.GetComponent<Melee>();
        range = player.GetComponent<Range>();
        magic = player.GetComponent<Magic>();

    }

    public void SwitchWeapon(weaponType weapon)
    {
        switch (weapon)
        {
            case weaponType.Magic:
                magic.enabled = true;
                melee.enabled = false;
                range.enabled = false;
                break;

            case weaponType.Melee:
                magic.enabled = false;
                melee.enabled = true;
                range.enabled = false;
                break;

            case weaponType.Range:
                magic.enabled = false;
                melee.enabled = false;
                range.enabled = true;
                break;
        }
    }
}
public enum weaponType { Range, Melee, Magic }
