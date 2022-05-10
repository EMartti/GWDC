using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SphereAttack : MonoBehaviour
{

    [SerializeField] private GameObject attackLocation;
    [SerializeField] private float sphereRadius = 1.5f;
    [SerializeField] private LayerMask layersToAttack;
    [SerializeField] private string tagsToAttack;
    [SerializeField] private int attackDamage;
    
    
    [SerializeField] private Player player;

    private void Start()
    {
        player = Player.Instance;
    }

    public void OnAnimAttack()
    {
        Collider[] hitColliders = Physics.OverlapSphere(attackLocation.gameObject.transform.position, sphereRadius, layersToAttack);

        foreach (Collider collider in hitColliders)
        {
            if (collider.gameObject.CompareTag(tagsToAttack))
            {
                IDamageable damageable = collider.gameObject.GetComponent<IDamageable>();

                if (damageable != null)
                {
                    damageable.TakeDamage(attackDamage, gameObject);
                }
            }
        }
    }
}
