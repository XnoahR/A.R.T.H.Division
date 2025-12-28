
using UnityEngine;


public class B4T : Enemy
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


    
    
}