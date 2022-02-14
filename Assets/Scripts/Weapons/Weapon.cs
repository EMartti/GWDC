using System;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.EventSystems;
using TMPro;

[RequireComponent(typeof(AudioSource))]
public class Weapon : MonoBehaviour
{
    public Transform firingPoint;
    [SerializeField] private GameObject bullet;    

    [SerializeField] private int magazineSize, bulletsPerTap;
    [SerializeField] private bool automatic;

    [SerializeField] private float shootForce, upwardForce;
    [SerializeField] private float timeBetweenShots, spread, reloadTime, timeBetweenShooting;

    AudioSource audioSource;
    [SerializeField] private AudioClip shootSound;
    [SerializeField] private AudioClip reload;
    [SerializeField] private GameObject muzzleFlash;
    public TextMeshProUGUI ammunnitionDisplay;

    int bulletsShot, bulletsLeft;
    bool shooting, readyToShoot, reloading;
    private bool allowInvoke = true;

    private PlayerInputActions playerInputActions;

    //public EquipmentSlots equipmentSlot;

    private void Awake()
    {        
        bulletsLeft = magazineSize;
        readyToShoot = true;

        audioSource = GetComponent<AudioSource>();
    }

    private void Start()
    {
        if(gameObject.tag == "Player")
        {
            playerInputActions = PlayerInputs.Instance.playerInputActions;

            playerInputActions.Player.Reload.Enable();
            playerInputActions.Player.Reload.started += OnReload;

            playerInputActions.Player.Fire2.Enable();
            playerInputActions.Player.Fire2.started += OnFire;
        }        
    }

    #region InputSystem
    
    private void OnFire(InputAction.CallbackContext obj)
    {
        shooting = true;
        if (readyToShoot && shooting && !reloading && !automatic)
        {
            bulletsShot = 0;
            Shoot();
        }
        shooting = false;
    }

    private void OnDisable()
    {
        if (gameObject.tag == "Player")
        {
            playerInputActions.Player.Fire2.Disable();
            playerInputActions.Player.Reload.Disable();
        }
    }

    //Reloading
    private void OnReload(InputAction.CallbackContext obj)
    {
        if (bulletsLeft < magazineSize && !reloading) Reload();
    }
    #endregion

    void Update()
    {
        shooting = false;
        if (automatic && playerInputActions.Player.Fire.ReadValue<float>() > 0)
            shooting = true;

        //Automatic firing
        if (readyToShoot && shooting && !reloading)
        {
            bulletsShot = 0;
            Shoot();
        }

        //Bullets left UI
        if (ammunnitionDisplay != null)
            ammunnitionDisplay.SetText(bulletsLeft / bulletsPerTap + "/" + magazineSize / bulletsPerTap);
    }

    public void Shoot()
    {
        if (bulletsLeft <= 0) { Reload(); return; }

        readyToShoot = false;

        Vector3 directionWithoutSpread = transform.forward;

        float x = UnityEngine.Random.Range(-spread, spread);
        float y = UnityEngine.Random.Range(-spread, spread);

        Vector3 directionWithSpread = directionWithoutSpread + new Vector3(x, y, 0);

        GameObject currentBullet = Instantiate(bullet, firingPoint.position, Quaternion.identity);
        currentBullet.transform.up = directionWithSpread.normalized;

        currentBullet.GetComponent<Rigidbody>().AddForce(transform.forward * shootForce, ForceMode.Impulse);
        currentBullet.GetComponent<Rigidbody>().AddForce(transform.up * upwardForce, ForceMode.Impulse);

        if (muzzleFlash != null)
            Instantiate(muzzleFlash, firingPoint.position, Quaternion.identity);

        bulletsLeft--;
        bulletsShot++;

        if (allowInvoke)
        {
            Invoke("ResetShot", timeBetweenShooting);
            allowInvoke = false;
        }

        if (bulletsShot < bulletsPerTap && bulletsLeft > 0)
            Invoke("Shoot", timeBetweenShots);

        if(shootSound != null) 
        audioSource.PlayOneShot(shootSound, 0.45F);
    }

    private void ResetShot()
    {
        readyToShoot = true;
        allowInvoke = true;
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
