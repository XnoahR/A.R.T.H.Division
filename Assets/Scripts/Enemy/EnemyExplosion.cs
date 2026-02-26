using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyExplosion : MonoBehaviour
{
    [SerializeField] float radius = 2f;
    [SerializeField] int damage = 25;
    [SerializeField] LayerMask DamageMask;
    [SerializeField] GameObject explodeVFX;
    void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(transform.position, radius);
    }
    public void Explode()
    {
        Collider2D[] hits = Physics2D.OverlapCircleAll(transform.position, radius, DamageMask);

        foreach (var hit in hits)
        {
            if (hit.TryGetComponent<IDamageable>(out var dmg))
            {
                dmg.TakeDamage(damage);
            }
        }
        Instantiate(explodeVFX, transform.position, Quaternion.identity);
        Destroy(gameObject);
    }
}
