using System.Collections;
using UnityEngine;

public class PartnerWeaponController : MonoBehaviour
{
    [Header("References")]
    [SerializeField] private Transform weaponContainer;
    [SerializeField] private Transform weaponPivot;
    [SerializeField] private GameObject currentWeapon;
    [SerializeField] private float visionRange = 3;
    [SerializeField] private LayerMask enemyLayer;
    private Weapon currentWeaponFunc;
    private PartnerRuntimeData runtimeData;
    [SerializeField] float scanInterval = 0.2f;

    float scanTimer;

    [Header("Stats")]
    private int currentAmmo;
    private int magazineCapacity;
    private float fireDelay;
    private bool isReloading;

    private float fireRate;
    private int attack;
    private float reloadSpeed;
    private float punchback;
    private float recoil;

    private Transform currentTarget;
    private GunData gunData;

    public void Init(PartnerRuntimeData data)
    {
        runtimeData = data;

        // Get scaled stats from runtimeData
        attack = data.GetAttack();
        fireRate = data.GetFireRate();
        reloadSpeed = data.GetReloadSpeed();
        punchback = data.GetPunchback();

        SetupWeapon(data.data.gunData);
    }

    void Update()
    {
        if (currentWeaponFunc == null) return;

        if (fireDelay > 0)
            fireDelay -= Time.deltaTime;

        scanTimer -= Time.deltaTime;

        if (scanTimer <= 0f)
        {
            FindTarget();
            scanTimer = scanInterval;
        }

        if (currentTarget != null)
        {
            RotateToTarget();
        }
        if (currentTarget != null && !currentTarget.gameObject.activeInHierarchy)
        {
            currentTarget = null;
        }

        if (currentAmmo <= 0 && !isReloading)
        {
            StartCoroutine(Reload());
            return;
        }

        if (fireDelay <= 0 && !isReloading && currentTarget != null)
        {
            Shoot();
            fireDelay = 1f / fireRate;
        }
    }

    void Shoot()
    {
        currentWeaponFunc.Fire(attack, 0.15f * recoil, punchback);
        currentAmmo--;
    }

    IEnumerator Reload()
    {
        isReloading = true;
        yield return new WaitForSeconds(gunData.reloadTime / (1 + (reloadSpeed * 0.075f)));
        currentAmmo = magazineCapacity;
        isReloading = false;
    }

    void SetupWeapon(GunData gunData)
    {
        if (currentWeapon != null)
            Destroy(currentWeapon);
        this.gunData = gunData;
        currentWeapon = Instantiate(gunData.WeaponGO, weaponContainer);

        currentWeaponFunc = currentWeapon.GetComponent<Weapon>();
        currentWeaponFunc.Init(gunData.bulletData);

        currentWeapon.transform.localPosition = Vector3.zero;
        currentWeapon.transform.localRotation = Quaternion.identity;

        magazineCapacity = gunData.magazineCapacity;
        currentAmmo = magazineCapacity;

        recoil = gunData.recoil;
    }

    void FindTarget()
    {
        Transform nearest = FindNearestEnemy();
        currentTarget = nearest != null ? nearest : null;
    }

    Transform FindNearestEnemy()
    {
        Collider2D[] hits = Physics2D.OverlapCircleAll(
            transform.position,
            visionRange,
            enemyLayer
        );

        float minDist = Mathf.Infinity;
        Transform nearest = null;

        foreach (var hit in hits)
        {
            float dist = Vector2.Distance(transform.position, hit.transform.position);
            if (dist < minDist)
            {
                minDist = dist;
                nearest = hit.transform;
            }
        }

        return nearest;
    }

    void RotateToTarget()
    {
        if (currentTarget == null) return;

        Vector2 dir = (currentTarget.position - weaponPivot.position).normalized;

        float angle = Mathf.Atan2(dir.y, dir.x) * Mathf.Rad2Deg;

        angle = Mathf.Max(angle, 0f);
        angle = Mathf.Clamp(angle, 0f, 90f);

        weaponPivot.rotation = Quaternion.Euler(0, 0, angle);
    }
}