using UnityEngine;

[CreateAssetMenu(menuName = "Game/Upgrade/Weapon Upgrade")]
public class WeaponUpgradeData : UpgradeData
{
   public GunData gunData;

   
   public override void Apply()
    {
        GameController.Instance.playerData.currentWeapon = gunData;
        UpgradeController.BroadcastWeaponUpgrade(gunData);
    }
}