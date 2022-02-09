using UnityEngine;

[RequireComponent(typeof(AudioSource))]
public class CustomBullet : MonoBehaviour
{
    public Rigidbody rb;
    public GameObject explosion;
    public LayerMask whatIsEnemies;
    AudioSource audioSource;
    [SerializeField] private AudioClip explodeSound;

    [Range(0f, 1f)]
    public float bounciness;
    public bool useGravity;

    public int explosionDamage;
    public float explosionRange;
    public float explosionForce;

    public int maxCollisions;
    public float maxLifeTime;
    public bool explodeOnTouch = true;

    int collisions;
    bool alreadyHitOnce = false;
    PhysicMaterial physics_mat;    

    private void Start()
    {
        Setup();
        audioSource = GetComponent<AudioSource>();
    }

    private void Update()
    {
        if (collisions > maxCollisions) Explode();

        maxLifeTime -= Time.deltaTime;
        if (maxLifeTime <= 0) Explode(); 

    }

    private void Explode()
    {
        if (!alreadyHitOnce)
        {
            if (explosion != null) Instantiate(explosion, transform.position, Quaternion.identity);

            Collider[] enemies = Physics.OverlapSphere(transform.position, explosionRange, whatIsEnemies);

            for (int i = 0; i < enemies.Length; i++)
            {
                if (enemies[i].GetComponent<Rigidbody>())
                    enemies[i].GetComponent<Rigidbody>().AddExplosionForce(explosionForce, transform.position, explosionRange);
                alreadyHitOnce = true;
            }
            if(explodeSound != null)
                AudioSource.PlayClipAtPoint(explodeSound, transform.position);
            Invoke("Delay", 0f);
        }
    }

    private void OnTriggerEnter(Collider collider)
    {
        IDamageable damageable = collider.GetComponent<IDamageable>();
        if (damageable != null)
        {
            damageable.TakeDamage(explosionDamage);
            Explode();
        }
            
            
    }

    private void Delay()
    {        
        Destroy(gameObject);
        alreadyHitOnce = false;
    }

    private void OnCollisionEnter(Collision collision)
    {
        IDamageable damageable = collision.gameObject.GetComponent<IDamageable>();
        if (damageable != null)
        {
            damageable.TakeDamage(explosionDamage);
            Explode();
        }
        /*
        collisions++;

        if (collision.collider.CompareTag("Enemy") && explodeOnTouch) Explode(); 
        */
    }

    private void Setup()
    {
        physics_mat = new PhysicMaterial();
        physics_mat.bounciness = bounciness;
        physics_mat.frictionCombine = PhysicMaterialCombine.Minimum;
        physics_mat.bounceCombine = PhysicMaterialCombine.Maximum;

        GetComponent<CapsuleCollider>().material = physics_mat;

        rb.useGravity = useGravity;
    }

    private void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(transform.position, explosionRange);
    }
}
