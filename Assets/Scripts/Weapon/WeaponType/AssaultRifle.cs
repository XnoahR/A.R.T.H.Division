using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AssaultRifle : Weapon
{
    // Start is called before the first frame update
    void Start()
    {
        canSwitchFireMode = true;
    }

    // Update is called once per frame
    void Update()
    {
        
    }
 
    void Awake()
    {
    }

    

    public override void Fire(int damage)
    {
        GameObject bulletGO = Instantiate(bulletData.BulletGO, firePoint.position, firePoint.rotation);
        bulletGO.GetComponent<BulletController>().Init(bulletData, damage);
        Destroy(bulletGO, 5);
    }
}
