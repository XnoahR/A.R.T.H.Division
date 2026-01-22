using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Pistol : Weapon
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
        GameObject bulletGO = Instantiate(bulletData.BulletGO, firePoint.position, firePoint.rotation);
        bulletGO.GetComponent<BulletController>().Init(bulletData, damage, knockback);
        Destroy(bulletGO, 4);
        StartCoroutine(MuzzleFlash());
        audioSource.PlayOneShot(shotSFX);
    }
}
