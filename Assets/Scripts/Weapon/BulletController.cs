using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BulletController : MonoBehaviour
{
   
    [SerializeField] private BulletData data;
    [SerializeField] private Rigidbody2D rb;
    [SerializeField] private int damage;
    [SerializeField] private GameObject bulletHitEffect;
    [SerializeField] private GameObject hitSound;
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
        if (collision.gameObject.layer == LayerMask.NameToLayer("Enemy"))
        {
            IDamageable damageable = collision.GetComponent<IDamageable>();
            if (damageable != null)
            {
                damageable.TakeDamage(damage);
                Instantiate(bulletHitEffect, transform.position, transform.rotation);
                GameObject hitSoundGO = Instantiate(hitSound, transform.position, transform.rotation);
                Destroy(hitSoundGO, 1);
                Destroy(gameObject);
            }
        }
        else{
        Instantiate(bulletHitEffect, transform.position, transform.rotation);
        Destroy(gameObject);
        }
    }
    
}
