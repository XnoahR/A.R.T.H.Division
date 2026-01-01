
using UnityEngine;


public class AirEnemy : Enemy, IFlyable, IShotable
{

    




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