using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BulletController : MonoBehaviour
{
    [SerializeField] float bulletSpeed;
    [SerializeField] private BulletData data;
    [SerializeField] private Rigidbody2D rb;
    [SerializeField] private int damage;
    // Start is called before the first frame update
    public void Init(BulletData bulletData, int damage)
    {
        data = bulletData;
        rb.velocity = transform.right*data.bulletSpeed;
        this.damage = damage;
    }
    void Awake()
    {
        rb = GetComponent<Rigidbody2D> ();
    }
    // Update is called once per frame

    void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Enemy"))
        {
            IDamageable damageable = collision.GetComponent<IDamageable>();
            if (damageable != null)
            {
                damageable.TakeDamage(damage);
                Destroy(gameObject);
            }
        }
        Destroy(gameObject);
    }
}
