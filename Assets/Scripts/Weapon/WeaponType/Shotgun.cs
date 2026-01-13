using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Shotgun : Weapon
{
    [SerializeField] int pelletCount = 5;
    [SerializeField] float spreadAngle = 10f;
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

    public override void Fire(int damage, float recoilOffset)
    {
        for (int i = 0; i < pelletCount; i++)
        {
            float randomAngle = Random.Range(-spreadAngle, spreadAngle);
            Quaternion spreadRotation =
       Quaternion.Euler(0f, 0f, firePoint.eulerAngles.z + randomAngle);

            GameObject bulletGO = Instantiate(bulletData.BulletGO, firePoint.position, spreadRotation);
            bulletGO.GetComponent<BulletController>().Init(bulletData, damage);
            Destroy(bulletGO, 4);
        }
        StartCoroutine(MuzzleFlash());
        audioSource.PlayOneShot(shotSFX);
    }
}
