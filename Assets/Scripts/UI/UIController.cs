using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
public class UIController : MonoBehaviour
{
    [SerializeField] WeaponController weaponController;
    [SerializeField] AmmoUI ammoUI;
    // Start is called before the first frame update
    void OnEnable()
    {
        weaponController.OnAmmoChange += ammoUI.UpdateAmmoUI;
        ammoUI.UpdateAmmoUI(weaponController.CurrentAmmo, weaponController.MagazineCapacity);
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
