using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SelectWeapon : MonoBehaviour
{
    private GameObject meleeWeapon;
    private GameObject rangeWeapon;
    private GameObject magicWeapon;

    [SerializeField] private GameObject thisWeaponVis;
    [SerializeField] private GameObject otherWeapon1Vis;
    [SerializeField] private GameObject otherWeapon2Vis;

    [SerializeField] private GameObject sword;
    [SerializeField] private GameObject bow;
    [SerializeField] private GameObject hammer;

    private Melee melee;
    private Weapon range;
    private Magic magic;

    private GameManager gameManager;


    private enum WeaponType { Melee, Range, Magic }
    [SerializeField] private WeaponType weapon;



    // Start is called before the first frame update
    void Start()
    {
        meleeWeapon = GameObject.Find("Player");
        rangeWeapon = GameObject.Find("BowWeapon");
        magicWeapon = GameObject.Find("Lightning");

        melee = meleeWeapon.GetComponent<Melee>();
        range = rangeWeapon.GetComponent<Weapon>();
        magic = magicWeapon.GetComponent<Magic>();

        gameManager = GameObject.Find("GameManager").GetComponent<GameManager>();

        sword = gameManager.sword;
        bow = gameManager.bow;
        hammer = gameManager.hammer;

        Invoke("DisableWeapons", 0.01f);
    }

    private void DisableWeapons()
    {
        melee.canUse = false;
        //meleeWeapon.SetActive(false);

        range.canUse = false;
        rangeWeapon.SetActive(false);

        magic.canUse = false;
        magicWeapon.SetActive(false);

        sword.SetActive(false);
        bow.SetActive(false);
        hammer.SetActive(false);
    }

    private void OnTriggerEnter(Collider other)
    {
        if(other.tag == "Player")
        {
            switch (weapon)
            {
                case WeaponType.Melee:
                    DisableWeapons();
                    //meleeWeapon.SetActive(true);
                    sword.SetActive(true);
                    melee.canUse = true;
                    break;
                case WeaponType.Range:
                    DisableWeapons();
                    bow.SetActive(true);
                    rangeWeapon.SetActive(true);
                    range.canUse = true;
                    break;
                case WeaponType.Magic:
                    DisableWeapons();
                    hammer.SetActive(true);
                    magicWeapon.SetActive(true);
                    magic.canUse = true;

                    break;
            }

            thisWeaponVis.SetActive(false);
            otherWeapon1Vis.SetActive(true);
            otherWeapon2Vis.SetActive(true);
        }
    }
}
