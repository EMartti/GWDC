using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Rigidbody))]
public class Knockback : MonoBehaviour
{
    [SerializeField] private float force = 10f;
    [Range(0f,10f)][SerializeField] private float damageMult = 1f;

    private Rigidbody rb;
    // Start is called before the first frame update
    void Start()
    {
        rb = GetComponent<Rigidbody>();
    }

    public void AddForce(int damage, Vector3 origin)
    {
        Vector3 dir = (transform.position - origin).normalized;
        Debug.Log("KnockBack" + dir);
        rb.AddForce(dir * force + dir * damage * damageMult, ForceMode.Impulse);
    }
}
