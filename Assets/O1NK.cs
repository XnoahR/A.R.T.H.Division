using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class O1NK : Enemy
{
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public override void Move()
    {
         transform.Translate(Vector3.left * enemyData.speed * Time.fixedDeltaTime);
    }
}
