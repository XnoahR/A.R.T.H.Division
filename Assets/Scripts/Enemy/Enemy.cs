using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public abstract class Enemy : MonoBehaviour
{
   
    public Transform firePoint;
    public BulletData bulletData;
    public EnemyData enemyData;
    public bool isSelfExplode;
    [SerializeField] protected Rigidbody2D rb;
    // Start is called before the first frame update
    protected void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        if (enemyData.isFly)
        {
            rb.gravityScale = 0;
        }
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public virtual void Move()
    {
         rb.velocity = new Vector2(-enemyData.speed, rb.velocity.y);
    }

    public virtual void Stop()
    {
        rb.velocity = Vector2.zero;
        Debug.Log("Stopp");
    }
    public abstract void Attack(Transform attackTarget, int damage);
}
