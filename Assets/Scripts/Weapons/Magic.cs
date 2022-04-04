using System;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.EventSystems;
using TMPro;

[RequireComponent(typeof(AudioSource))]
public class Magic : MonoBehaviour {
    [SerializeField] private GameObject magic;

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
    private bool allowInvoke = true;

    private PlayerInputActions playerInputActions;

    [SerializeField] private float explosionRange = 1f;
    public LayerMask whatIsEnemies;

    [SerializeField] private float lifeTime = 1f;

    private Plane plane;
    Ray ray;
    private Vector3 castPoint;
    [SerializeField] private int damage = 75;
    private GameObject currentMagic;

    private int layerMask;

    public bool canUse = true;

    //public EquipmentSlots equipmentSlot;

    private void Awake() {
        bulletsLeft = magazineSize;
        readyToShoot = true;

        audioSource = GetComponent<AudioSource>();

        layerMask = LayerMask.GetMask("Environment");
    }

    private void Start() {
        aM = AudioManager.Instance;
        if (shootSound == null) {
            shootSound = aM.sfxFireballStart;
        }

        if (gameObject.tag == "Player") {
            playerInputActions = PlayerInputs.Instance.playerInputActions;

            playerInputActions.Player.Reload.Enable();
            playerInputActions.Player.Reload.started += OnReload;

            playerInputActions.Player.Fire.Enable();
            playerInputActions.Player.Fire.started += OnFire;
        }

        playerInputActions.Player.MousePosition.Enable();
        plane = new Plane(Vector3.up, Vector3.zero);
        ray = Camera.main.ScreenPointToRay(Mouse.current.position.ReadValue());
    }

    #region InputSystem

    private void OnFire(InputAction.CallbackContext obj) {
        shooting = true;
        if (readyToShoot && shooting && !reloading && !automatic) {
            bulletsShot = 0;
            if(canUse)
                Shoot();
        }
        shooting = false;
    }

    //private void OnDisable()
    //{
    //    if (gameObject.tag == "Player")
    //    {
    //        playerInputActions.Player.Fire.Disable();
    //        playerInputActions.Player.Reload.Disable();
    //    }
    //}

    //Reloading
    private void OnReload(InputAction.CallbackContext obj) {
        if (bulletsLeft < magazineSize && !reloading) Reload();
    }
    #endregion

    void Update() {
        shooting = false;
        if (automatic && playerInputActions.Player.Fire.ReadValue<float>() > 0)
            shooting = true;

        //Automatic firing
        if (readyToShoot && shooting && !reloading) {
            bulletsShot = 0;
            Shoot();
        }

        //Bullets left UI
        if (ammunnitionDisplay != null)
            ammunnitionDisplay.SetText(bulletsLeft / projectilesPerTap + "/" + magazineSize / projectilesPerTap);
    }


    public void Shoot() {

        if (bulletsLeft <= 0) { Reload(); return; }

        readyToShoot = false;

        //Debug.Log("fired magic");
        ray = Camera.main.ScreenPointToRay(Mouse.current.position.ReadValue());

        RaycastHit hit;

        if (Physics.Raycast(ray, out hit, 1000f, layerMask)) 
        {
            castPoint = hit.point;
        } 
        else
            castPoint = transform.position;

        Explode();

        bulletsLeft--;
        bulletsShot++;

        if (allowInvoke) {
            Invoke("ResetShot", timeBetweenShooting);
            allowInvoke = false;
        }

        if (bulletsShot < projectilesPerTap && bulletsLeft > 0)
            Invoke("Shoot", timeBetweenShots);

        if (shootSound != null)
            audioSource.PlayOneShot(shootSound, 0.99F);
    }

    private void Explode() {
        currentMagic = Instantiate(magic, castPoint, Quaternion.Euler(-90, 0, 0));

        Collider[] enemies = Physics.OverlapSphere(castPoint, explosionRange, whatIsEnemies);

        foreach (Collider enemy in enemies) {
            if (enemy.gameObject.GetComponent<Health>() != null)
                enemy.gameObject.GetComponent<Health>().TakeDamage(damage, gameObject);
        }



        Invoke("Delay", lifeTime);
    }

    private void ResetShot() {
        readyToShoot = true;
        allowInvoke = true;
    }

    private void Reload() {
        reloading = true;
        if (reload != null)
            audioSource.PlayOneShot(reload, 0.7F);
        Invoke("ReloadFinished", reloadTime);
    }

    private void ReloadFinished() {
        bulletsLeft = magazineSize;
        reloading = false;
    }

    private void Delay() {
        Destroy(currentMagic);
    }

    // AI Shoot
    public void AiFire() {
        shooting = true;
        if (readyToShoot && shooting && !reloading && !automatic) {
            bulletsShot = 0;
            Shoot();
        }
        shooting = false;
    }
}
