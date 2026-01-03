using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
public class UIController : MonoBehaviour
{
    [SerializeField] WeaponController weaponController;
    [SerializeField] DayController dayController;
    [SerializeField] GameObject UpgradeUI;
    [SerializeField] AmmoUI ammoUI;
    [SerializeField] DayUI dayUI;
    // Start is called before the first frame update
    void OnEnable()
    {
        DayController.OnDayEnded += UpgradeMode;
        GameController.OnGameStart += GameMode;
        weaponController.OnAmmoChange += ammoUI.UpdateAmmoUI;
        dayController.OnDayChanged += dayUI.UpdateDayUI;
        ammoUI.UpdateAmmoUI(weaponController.CurrentAmmo, weaponController.MagazineCapacity);
        dayUI.UpdateDayUI(dayController.currentDay);
    }
    void GameMode()
    {
        UpgradeUI.SetActive(false);
    }
    void UpgradeMode()
    {
        UpgradeUI.SetActive(true);
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
