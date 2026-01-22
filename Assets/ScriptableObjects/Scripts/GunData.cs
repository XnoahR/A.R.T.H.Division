using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

[CreateAssetMenu(menuName = "GunData")]
public class GunData : ScriptableObject
{
    [Header("Info")]
    public string GunName;
    public GUN_TIPE gunType;
    public SHOT_MODE shotMode;
    public int durability;
    public bool isPermanent;

    [Header("Stats")]
    public float fireRate;
    public int damage;
    public int magazineCapacity;
    public float punchback;
    public int maxAmmo;
    public float reloadTime;
    public float recoil;


    [Header("References")]
    public BulletData bulletData;
    public GameObject WeaponGO;
    public Sprite gunSprite;



    public enum GUN_TIPE
    {
        ASSAULT,
        SMG,
        SHOTGUN,
        SNIPER,
        PISTOL
    }
    public enum SHOT_MODE
    {
        FULLAUTO,
        BURST,
        SINGLE
    }

    public void decreaseDurability()
    {
        if (!isPermanent) durability -= 1;
    }
}