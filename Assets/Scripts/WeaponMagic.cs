using System;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.EventSystems;
using TMPro;

[RequireComponent(typeof(AudioSource))]
public class WeaponMagic : MonoBehaviour
{
    [SerializeField] private GameObject magicPrefab;

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
    private bool allowInvoke = true;

    [SerializeField] private float explosionRange = 1f;
    public LayerMask whatIsEnemies;

    [SerializeField] private float lifeTime = 1f;

    [SerializeField] private int damage = 75;
    private GameObject currentMagic;

    public bool canUse = true;

    public GameObject parent;
    public Transform target;

    //public EquipmentSlots equipmentSlot;

    private void Start()
    {
        aM = AudioManager.Instance;

        if (shootSound == null)
        {
            shootSound = aM.sfxFireballStart;
        }

        audioSource = GetComponent<AudioSource>();
    }

    public void Attack()
    {
        bulletsLeft--;
        bulletsShot++;

        Invoke("KillParticles", lifeTime);

        if (shootSound != null)
            audioSource.PlayOneShot(shootSound, 0.99F);

        currentMagic = Instantiate(magicPrefab, target.position, Quaternion.Euler(-90, 0, 0));

        Collider[] enemies = Physics.OverlapSphere(target.position, explosionRange, whatIsEnemies);

        foreach (Collider enemy in enemies)
        {
            if (enemy.gameObject.GetComponent<Health>() != null)
                enemy.gameObject.GetComponent<Health>().TakeDamage(damage, gameObject);
        }

        Debug.Log("Fired magic");
    }

    private void KillParticles()
    {
        Destroy(currentMagic);
    }
}
