using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WeaponController : MonoBehaviour
{
    [Header("References")]
    [SerializeField] private Transform weaponContainer;
    [SerializeField] private Transform weaponPivot;
    [SerializeField] private Transform armPivot;
    [SerializeField] private GameObject currentWeapon;
    [SerializeField] public GunData gunData;
    [SerializeField] private PlayerController playerController;
    [SerializeField] private Weapon currentWeaponFunc;

    public event Action<int, int> OnAmmoChange;

    [Header("Utilities")]
    [SerializeField] int currentAmmo;
    [SerializeField] int magazineCapacity;
    [SerializeField] bool canShoot;
    [SerializeField] bool isReloading;
    [SerializeField] private float fireDelay;
    public int CurrentAmmo => currentAmmo;
    public int MagazineCapacity => magazineCapacity;
    public Vector2 aimDirection { get; private set; }



    public float aimSpeed { get; }

    // Start is called before the first frame update
    void Awake()
    {
        playerController = GetComponent<PlayerController>();
        if (gunData == null)
        {
            Debug.LogError("GunData is NULL!", this);
            return;
        }

        if (weaponContainer == null)
        {
            Debug.LogError("WeaponContainer is NULL!", this);
            return;
        }
    }

    void OnEnable()
    {
        GameController.OnGameStart += Init;
    }
    
    public void Init()
    {
        SetupWeapon(gunData);
    }

    // Update is called once per frame
    void Update()
    {
        if (playerController.canPlay)
        {
            if (InputMode())
            {
                if (currentAmmo < 1)
                {
                    return;
                }
                if (fireDelay <= 0 && canShoot && !isReloading)
                {
                    currentWeaponFunc.Fire(CalculateDamage());
                    DecreaseAmmo();
                    fireDelay = 1 / (gunData.fireRate * (1 + (playerController.FireRateLevel*0.1f)));
                    Debug.Log(fireDelay);
                }
            }

            if (Input.GetKeyDown(KeyCode.R) && currentAmmo < magazineCapacity)
            {
                StartCoroutine(Reload());
            }

            if (Input.GetKeyDown(KeyCode.B))
            {
                currentWeaponFunc.SwitchFireMode(currentWeaponFunc.FireMode);
                Debug.Log($"Changed to : ${currentWeaponFunc.FireMode}");
            }

            Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
            Plane plane = new Plane(Vector3.forward, weaponPivot.position);

            if (plane.Raycast(ray, out float enter))
            {
                Vector3 hitPoint = ray.GetPoint(enter);
                Vector2 direction = (hitPoint - weaponPivot.position).normalized;
                aimDirection = direction;
                float angle = Mathf.Atan2(direction.y, direction.x) * Mathf.Rad2Deg;
                weaponPivot.rotation = Quaternion.Euler(0, 0, angle);
            }
        }
        if (fireDelay > 0)
        {
            fireDelay -= Time.deltaTime;
        }


        if ((aimDirection.x < 0 && playerController.isRight) || (aimDirection.x > 0 && !playerController.isRight))
        {
            playerController.Flip(weaponPivot);
        }
    }

    void DecreaseAmmo()
    {
        currentAmmo--;
        OnAmmoChange?.Invoke(currentAmmo, magazineCapacity);
    }

    public int CalculateDamage()
    {
        int damage =  Mathf.FloorToInt(
            (gunData.damage + gunData.bulletData.additionalDamage)
            * (1 + (playerController.AttackLevel * 0.1f))
        );

        Debug.Log(damage);
        return damage;
    }

    IEnumerator Reload()
    {
        isReloading = true;
        yield return new WaitForSeconds(gunData.reloadTime / (1 + (playerController.ReloadSpeedLevel*0.075f)));
        currentAmmo = magazineCapacity;
        OnAmmoChange?.Invoke(currentAmmo, magazineCapacity);
        isReloading = false;

    }

    private bool InputMode()
    {
        bool input = currentWeaponFunc.FireMode == Weapon.FIREMODE.FULLAUTO ? Input.GetButton("Fire1") : Input.GetButtonDown("Fire1");
        return input;
    }
    void SetupWeapon(GunData gunData)
    {
        currentWeapon = Instantiate(gunData.WeaponGO, weaponContainer);
        currentWeaponFunc = currentWeapon.GetComponent<Weapon>();
        currentWeaponFunc.Init(gunData.bulletData);
        currentWeapon.transform.localPosition = Vector3.zero;
        currentWeapon.transform.localRotation = Quaternion.identity;
        magazineCapacity = Mathf.CeilToInt(gunData.magazineCapacity * (1 + (playerController.MagazineCapacityLevel * 0.075f)));
        currentAmmo = magazineCapacity;
        OnAmmoChange?.Invoke(currentAmmo, magazineCapacity);
        canShoot = true;
    }
}
