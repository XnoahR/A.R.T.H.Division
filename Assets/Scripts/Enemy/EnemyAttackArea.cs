using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyAttackArea: MonoBehaviour
{
    private int damage;
    void Awake()
    {
        damage = gameObject.GetComponentInParent<Enemy>().enemyData.damage;
    }
    void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Objective"))
        {
            IDamageable damageable = collision.GetComponent<IDamageable>();
            if(damageable != null)
            {
                damageable.TakeDamage(damage);
            }
        }
    }
}
