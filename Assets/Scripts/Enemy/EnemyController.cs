using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyController : MonoBehaviour, IDamageable
{
    [SerializeField] private float health = 100;
    [SerializeField] private float MAX_HEALTH = 100;
    [SerializeField] private float enemySpeed = 5;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    void FixedUpdate()
    {
        transform.Translate(Vector2.left*enemySpeed*Time.fixedDeltaTime);
    }
    public void TakeDamage(int damage)
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
}
