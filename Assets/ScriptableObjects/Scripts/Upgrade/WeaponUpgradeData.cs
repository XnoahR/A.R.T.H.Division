using UnityEngine;

[CreateAssetMenu(menuName = "Upgrade/Weapon Upgrade")]
public class WeaponUpgradeData : UpgradeData
{
   public GunData gunData;

   
   public override void Apply(GameObject target)
    {
        target.GetComponent<WeaponController>().ChangeWeapon(gunData);
    }
}