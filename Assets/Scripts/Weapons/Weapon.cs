using System;
using UnityEngine;
using TMPro;

[RequireComponent(typeof(AudioSource))]
public class Weapon : MonoBehaviour
{
    public Transform firingPoint;
    public Transform target;
    [SerializeField] private GameObject projectilePrefab;    

    [SerializeField] private int magazineSize, projectilesPerTap;
    [SerializeField] private bool automatic;

    [SerializeField] private float shootForce, upwardForce;
    [SerializeField] private float timeBetweenShots, spread, reloadTime, timeBetweenShooting;

    AudioSource audioSource;
    AudioManager aM;
    [SerializeField] private AudioClip shootSound;
    [SerializeField] private AudioClip reload;
    [SerializeField] private GameObject muzzleFlash;
    public TextMeshProUGUI ammunnitionDisplay;

    int bulletsShot, bulletsLeft;
    bool shooting, readyToShoot, reloading;

    private PlayerInputActions playerInputActions;

    [HideInInspector] public GameObject parent;

    private void Awake()
    {        
        bulletsLeft = magazineSize;
        readyToShoot = true;

        audioSource = GetComponent<AudioSource>();
    }

    private void Start()
    {
        aM = AudioManager.Instance;
        shootSound = aM.sfxArrowStart;            
    }

    public void Shoot()
    {
        if (bulletsLeft <= 0) { Reload(); return; }

        readyToShoot = false;

        Vector3 directionWithoutSpread = (target.position - parent.transform.position).normalized;

        float x = UnityEngine.Random.Range(-spread, spread);
        float y = UnityEngine.Random.Range(-spread, spread);

       // Vector3 directionWithSpread = directionWithoutSpread + new Vector3(x, y, 0);

        GameObject currentBullet = Instantiate(projectilePrefab, firingPoint.position, Quaternion.identity);
        currentBullet.transform.up = directionWithoutSpread.normalized;

        currentBullet.GetComponent<Rigidbody>().AddForce(directionWithoutSpread * shootForce, ForceMode.Impulse);
        currentBullet.GetComponent<Rigidbody>().AddForce(parent.transform.up * upwardForce, ForceMode.Impulse);
        
        // Add player meta-level damagebonus to projectile
        if (gameObject.tag == "Player")
            currentBullet.GetComponent<CustomBullet>().explosionDamage += PlayerStats.Instance.damageBonus;

        if (muzzleFlash != null)
            Instantiate(muzzleFlash, firingPoint.position, Quaternion.identity);

        bulletsLeft--;
        bulletsShot++;

        if (bulletsShot < projectilesPerTap && bulletsLeft > 0)
            Invoke("Shoot", timeBetweenShots);

        if(shootSound != null) 
        audioSource.PlayOneShot(shootSound, 0.45F);
    }    

    private void Reload()
    {
        reloading = true;
        if(reload != null)
            audioSource.PlayOneShot(reload, 0.7F);
        Invoke("ReloadFinished", reloadTime);
    }

    private void ReloadFinished()
    {
        bulletsLeft = magazineSize;
        reloading = false;
    }

    // AI Shoot
    public void AiFire()
    {
        shooting = true;
        if (readyToShoot && shooting && !reloading && !automatic)
        {
            bulletsShot = 0;
            Shoot();
        }
        shooting = false;
    }
}
