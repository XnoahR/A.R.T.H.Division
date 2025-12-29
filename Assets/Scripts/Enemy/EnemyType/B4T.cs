
using UnityEngine;


public class B4T : Enemy, IFlyable, IShotable
{

    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        if (enemyData.isFly)
        {
            rb.gravityScale = 0;
        }
    }

    public override void Move()
    {
       transform.Translate(Vector3.left * enemyData.speed * Time.fixedDeltaTime);
    }


    public override void Attack(Transform attackTarget, int damage)
    {
        Fire(attackTarget, damage);
    }

    public virtual void Fire(Transform attackTarget, int damage)
    {
        GameObject bulletGO = Instantiate(bulletData.BulletGO, firePoint.position, firePoint.rotation);
        bulletGO.GetComponent<EnemyBulletController>().Init(bulletData, damage);
    }
    
}