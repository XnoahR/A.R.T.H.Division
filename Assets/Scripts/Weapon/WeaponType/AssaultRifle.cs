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
        audioSource = GetComponent<AudioSource>();
    }

    

    public override void Fire(int damage, float recoilOffset)
    {
        float spread = Random.Range(-recoilOffset, recoilOffset);
        Quaternion spreadRotation = firePoint.rotation * Quaternion.Euler(0f, 0f, spread);
        Debug.Log(spreadRotation);
        GameObject bulletGO = Instantiate(bulletData.BulletGO, firePoint.position, spreadRotation);
        bulletGO.GetComponent<BulletController>().Init(bulletData, damage);
        Destroy(bulletGO, 4);
        StartCoroutine(MuzzleFlash());
        audioSource.PlayOneShot(shotSFX);
    }
}
