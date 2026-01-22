using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SubMachineGun : Weapon
{
    // Start is called before the first frame update
    void Start()
    {
    }

    // Update is called once per frame
    void Update()
    {

    }

    void Awake()
    {
    }

    public override void Fire(int damage, float recoilOffset, float knockback)
    {
        float spread = Random.Range(-recoilOffset, recoilOffset);
        Quaternion spreadRotation = firePoint.rotation * Quaternion.Euler(0f, 0f, spread);
        GameObject bulletGO = Instantiate(bulletData.BulletGO, firePoint.position, spreadRotation);
        bulletGO.GetComponent<BulletController>().Init(bulletData, damage, knockback);
        Destroy(bulletGO, 5);
    }
}
