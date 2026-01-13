using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class EnemyController : MonoBehaviour, IDamageable
{
     public static event Action OnEnemyDead;
     public event Action<int> onEnemyDamaged;
    public Enemy enemyScript;
    public int health;
    public int speed;
    bool isDie = false;
    bool isAttacking = false;
    // [SerializeField] private BoxCollider2D enemyHitCollider;
    [SerializeField] private BoxCollider2D enemyTriggerObjective;
    [SerializeField] public bool isTargetDetected = false;
    public Transform attackTarget;
    // Start is called before the first frame update
    void OnEnable()
    {
        InitEnemy();
    }

    // Update is called once per frame
    void Update()
    {

    }

    void FixedUpdate()
    {
        if (!isTargetDetected && !isAttacking)
        {
            enemyScript.Move();
        }
    }


    public void TakeDamage(int damage)
    {
        health -= damage;
        onEnemyDamaged?.Invoke(health);
        if (health <= 0)
        {
            if(isDie) return;
            isDie = true;
            Die();
        }
    }
    private void Die()
    {
        OnEnemyDead?.Invoke();
        Destroy(gameObject);
    }

    private void InitEnemy()
    {
        enemyScript = GetComponent<Enemy>();
        health = enemyScript.enemyData.health;
        speed = enemyScript.enemyData.speed;
    }

    public IEnumerator AttackTarget()
    {
        while (isTargetDetected == true)
        {
            isAttacking = true;
            yield return new WaitForSeconds(enemyScript.enemyData.attackSpeed);
            enemyScript.Attack(attackTarget, enemyScript.enemyData.damage);
            isAttacking = false;
        }
    }

}
