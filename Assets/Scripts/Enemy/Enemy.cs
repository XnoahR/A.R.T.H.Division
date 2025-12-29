using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class Enemy : MonoBehaviour
{
    public Transform firePoint;
    public BulletData bulletData;
    public EnemyData enemyData;
    [SerializeField] protected Rigidbody2D rb;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public abstract void Move();
    public abstract void Attack(Transform attackTarget, int damage);
}
