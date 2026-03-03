using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyAttackArea : MonoBehaviour
{
    private int damage;
    private TARGET attackTarget;
    private string target;
    public bool IsTargetInside { get; private set; }
    void Awake()
    {
        Enemy enemy = GetComponentInParent<Enemy>();
        damage = enemy.enemyData.damage;
        attackTarget = enemy.enemyData.attackTarget;
        target = attackTarget == TARGET.PLAYER ? "Player" : "Objective";
    }
    // void OnTriggerEnter2D(Collider2D collision)
    // {
    //     if (collision.CompareTag(target))
    //     {
    //         IDamageable damageable = collision.GetComponent<IDamageable>();
    //         if (damageable != null)
    //         {
    //             damageable.TakeDamage(damage);
    //         }
    //     }

    // }

    void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag(target))
            IsTargetInside = true;
    }

    void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.CompareTag(target))
            IsTargetInside = false;
    }
}
