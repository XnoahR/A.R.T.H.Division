using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyController : MonoBehaviour, IDamageable
{
    public Enemy enemyScript;
    public int health;
    public int speed;
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
       enemyScript.Move();
    }
    
    
    public virtual void TakeDamage(int damage)
    {
        health -= damage;
        Debug.Log("Health left: " + health);
        if(health <= 0)
        {
            Die();
        }
    }
    private void Die()
    {
        Destroy(gameObject);
    }

    private void InitEnemy()
    {
        enemyScript = GetComponent<Enemy>();
        health = enemyScript.enemyData.health;
        speed = enemyScript.enemyData.speed;
    }
}
