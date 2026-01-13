using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "GunData")]
public class GunData : ScriptableObject
{
    [Header("Info")]
    public string GunName;
    public GUN_TIPE gunType;
    public SHOT_MODE shotMode;

    [Header("Stats")]
    public float fireRate;
    public int damage;
    public int magazineCapacity;
    public int punchback;
    public int maxAmmo;
    public float reloadTime;
    public float recoil;
  

    [Header("References")]
    public BulletData bulletData;
    public GameObject WeaponGO;
    


      public enum GUN_TIPE
    {
        ASSAULT,
        SMG,
        SHOTGUN,
        SNIPER,
        SPECIAL
    }
    public enum SHOT_MODE
    {
        FULLAUTO,
        BURST,
        SINGLE
    }
}