using UnityEngine;

[CreateAssetMenu(menuName = "Game/Upgrade/Weapon Upgrade")]
public class WeaponUpgradeData : UpgradeData
{
   public GunData gunData;

   
   public override void Apply(GameObject target)
    {
        target.GetComponent<WeaponController>().ChangeWeapon(gunData);
    }
}