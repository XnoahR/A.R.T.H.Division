using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class EnemyController : MonoBehaviour, IDamageable, Iknockable
{
    public static event Action OnEnemyDead;
    public event Action<int> onEnemyDamaged;

    [Header("References")]
    public Enemy enemyScript;
    public Transform attackTarget;

    [Header("Stats")]
    public int health;
    public int speed;

    [Header("State")]
    public bool isTargetDetected = false;
    public bool isAttacking = false;
    public bool isKnockedBack = false;
    bool isDie = false;

    [SerializeField] EnemyExplosion exploder;
    [SerializeField] private BoxCollider2D enemyTriggerObjective;

    Rigidbody2D rb;
    Coroutine attackRoutine;

    void Awake()
    {
        exploder = GetComponent<EnemyExplosion>();
        rb = GetComponent<Rigidbody2D>();
        enemyScript = GetComponent<Enemy>();
    }

    void OnEnable()
    {
        InitEnemy();
    }

    void FixedUpdate()
    {
        // Musuh hanya jalan kalau dia TIDAK detect target dan TIDAK knockback
        if (!isTargetDetected && !isKnockedBack)
        {
            enemyScript.Move();
        }
    }


    #region DAMAGE
    public void TakeDamage(int damage)
    {
        health -= damage;
        onEnemyDamaged?.Invoke(health);

        if (health <= 0 && !isDie)
        {
            isDie = true;
            if (exploder != null) Explode();
            else Die();
        }
    }

    void Die()
    {
        StopAttackSafe();
        OnEnemyDead?.Invoke();
        Destroy(gameObject);
    }

    void Explode()
    {
        StopAttackSafe();
        OnEnemyDead?.Invoke();
        exploder.Explode();
        Debug.Log("Boom");
    }
    #endregion

    #region KNOCKBACK
    public void ApplyKnockback(Vector2 dir, float force)
    {
        // hanya hentikan attack coroutine sementara
        if (attackRoutine != null)
        {
            StopCoroutine(attackRoutine);
            attackRoutine = null;
        }

        isKnockedBack = true;
        rb.AddForce(dir * (force / enemyScript.enemyData.knockbackResistance), ForceMode2D.Impulse);

        StartCoroutine(RecoverFromKnockback());
    }

    IEnumerator RecoverFromKnockback()
    {
        yield return new WaitForSeconds(0.15f);
        isKnockedBack = false;

        // lanjutkan attack jika masih detect target
        if (isTargetDetected && attackRoutine == null)
        {
            attackRoutine = StartCoroutine(AttackTarget());
        }
    }

    #endregion

    #region ATTACK CONTROL
    public void StartAttackSafe(Transform target)
    {
        if (attackRoutine != null) return;        // already attacking
        if (isKnockedBack) return;                // cannot attack during knockback
        Debug.Log("Attack");
        attackTarget = target;
        isTargetDetected = true;
        attackRoutine = StartCoroutine(AttackTarget());
    }

    public void StopAttackSafe()
    {
        if (attackRoutine != null)
        {
            StopCoroutine(attackRoutine);
            attackRoutine = null;
        }
        isAttacking = false;
        attackTarget = null; // hanya reset target
    }


    public IEnumerator AttackTarget()
    {
        while (isTargetDetected && !isKnockedBack)
        {
            if (attackTarget == null)
            {
                StopAttackSafe();
                yield break;
            }

            // Tidak pakai attackRange
            isAttacking = true;
            rb.velocity = Vector2.zero; // stop movement while attacking

            yield return new WaitForSeconds(enemyScript.enemyData.attackSpeed);

            enemyScript.Attack(attackTarget, enemyScript.enemyData.damage);
            isAttacking = false;
        }
    }

    #endregion

    private void InitEnemy()
    {
        health = enemyScript.enemyData.health;
        speed = enemyScript.enemyData.speed;
    }
}
