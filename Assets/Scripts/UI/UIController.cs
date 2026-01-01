using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
public class UIController : MonoBehaviour
{
    [SerializeField] WeaponController weaponController;
    [SerializeField] DayController dayController;
    [SerializeField] AmmoUI ammoUI;
    [SerializeField] DayUI dayUI;
    // Start is called before the first frame update
    void OnEnable()
    {
        weaponController.OnAmmoChange += ammoUI.UpdateAmmoUI;
        dayController.OnDayChanged += dayUI.UpdateDayUI;
        ammoUI.UpdateAmmoUI(weaponController.CurrentAmmo, weaponController.MagazineCapacity);
        dayUI.UpdateDayUI(dayController.currentDay);
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
