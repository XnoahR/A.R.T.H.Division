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
    [SerializeField] public GunData pistolData;
    [SerializeField] private PlayerController playerController;
    [SerializeField] private Weapon currentWeaponFunc;

    public event Action<int, int> OnAmmoChange;
    public static event Action<GunData> onWeaponChange;
    public static event Action<bool, int> OnDurabilityChange;

    [Header("Utilities")]
    [SerializeField] int currentAmmo;
    [SerializeField] int magazineCapacity;
    [SerializeField] bool canShoot;
    [SerializeField] bool isReloading;
    [SerializeField] private float fireDelay;
    private bool hasInitializedDurability = false;

    [SerializeField] private bool isPermanent;
    public bool IsPermanent => isPermanent;
    [SerializeField] private int currentWeaponDurability;
    public int CurrentWeaponDurability => currentWeaponDurability;
    public int CurrentAmmo => currentAmmo;
    public int MagazineCapacity => magazineCapacity;
    public Vector2 aimDirection { get; private set; }
    private bool isFocus;
    private float recoilOffset;



    public float aimSpeed { get; }

    // Start is called before the first frame update
    void Awake()
    {
        playerController = GetComponent<PlayerController>();
        gunData = pistolData;
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
        DayController.OnDayEnded += decreaseDurability;
    }
    void OnDisable()
    {
        GameController.OnGameStart -= Init;
        DayController.OnDayEnded -= decreaseDurability;
    }

    public void Init()
    {
        SetupWeapon(gunData);
        Debug.Log($"Init weapon: {gunData.name}, permanent: {gunData.isPermanent}");

        if (!hasInitializedDurability)
        {
            currentWeaponDurability = gunData.durability;
            hasInitializedDurability = true;
            OnDurabilityChange?.Invoke(isPermanent, currentWeaponDurability);
        }
    }

    // Update is called once per frame
    void Update()
    {
        if (currentWeaponFunc == null)
            return;
        isFocus = Input.GetMouseButton(1);
        if (playerController.canPlay)
        {
            if (InputMode())
            {
                if (currentAmmo < 1)
                {
                    StartCoroutine(Reload());
                    return;
                }
                if (fireDelay <= 0 && canShoot && !isReloading)
                {
                    float spread = recoilOffset;
                    if (isFocus) spread *= 0.2f;
                    currentWeaponFunc.Fire(CalculateDamage(), spread, CalculateKnockback());
                    DecreaseAmmo();
                    fireDelay = 1 / (gunData.fireRate * (1 + (playerController.FireRateLevel * 0.1f)));
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
        return Mathf.FloorToInt(
            (gunData.damage + gunData.bulletData.additionalDamage)
            * (1 + (playerController.AttackLevel * 0.1f))
        );

    }

    public float CalculateKnockback()
    {
        return gunData.punchback * (1 + (playerController.punchbackLevel * 0.1f));

    }

    IEnumerator Reload()
    {
        isReloading = true;
        yield return new WaitForSeconds(gunData.reloadTime / (1 + (playerController.ReloadSpeedLevel * 0.075f)));
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
        if (currentWeapon != null)
            Destroy(currentWeapon);

        currentWeapon = Instantiate(gunData.WeaponGO, weaponContainer);

        currentWeaponFunc = currentWeapon.GetComponent<Weapon>();
        currentWeaponFunc.Init(gunData.bulletData);

        currentWeapon.transform.localPosition = Vector3.zero;
        currentWeapon.transform.localRotation = Quaternion.identity;

        magazineCapacity = Mathf.CeilToInt(
            gunData.magazineCapacity * (1 + playerController.MagazineCapacityLevel * 0.075f)
        );

        currentAmmo = magazineCapacity;
        recoilOffset = gunData.recoil;
        isPermanent = gunData.isPermanent;

        onWeaponChange?.Invoke(gunData);
        OnAmmoChange?.Invoke(currentAmmo, magazineCapacity);

        canShoot = true;
    }

    public void ChangeWeapon(GunData newGunData)
    {
        if (weaponContainer.childCount > 0)
            Destroy(weaponContainer.GetChild(0).gameObject);

        currentWeapon = null;
        gunData = newGunData;

        hasInitializedDurability = false;
        currentWeaponDurability = gunData.durability;

        SetupWeapon(gunData);

        OnDurabilityChange?.Invoke(isPermanent, currentWeaponDurability);
    }

    public void decreaseDurability()
    {
        if (isPermanent || !hasInitializedDurability)
            return;

        currentWeaponDurability--;
        OnDurabilityChange?.Invoke(isPermanent, currentWeaponDurability);

        if (currentWeaponDurability < 0)
        {
            ChangeWeapon(pistolData);
        }
    }
}
