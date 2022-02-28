using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public interface IDamageable
{
    void TakeDamage(int damage, Vector3 origin);
    void AddHealth(int healValue);
}
