using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyBulletController : MonoBehaviour
{
    [SerializeField] private int damage;
    [SerializeField] private Rigidbody2D rb;
    // Start is called before the first frame update
    public void Init(BulletData bulletData, int damage)
    {
        rb = GetComponent<Rigidbody2D>();
        this.damage = damage;
        rb.velocity = transform.right*-1*bulletData.bulletSpeed;
    }

    void Awake()
    {
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        
    }

    void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Objective"))
        {
             IDamageable damageable = collision.GetComponent<IDamageable>();
             if(damageable != null)
            {
                damageable.TakeDamage(damage);
                Destroy(gameObject);
            }
        }
    }
}
