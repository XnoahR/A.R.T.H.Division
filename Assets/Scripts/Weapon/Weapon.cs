using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class Weapon : MonoBehaviour
{
    [SerializeField] protected Transform firePoint;
    [SerializeField] protected BulletData bulletData;
    public enum FIREMODE
    {
        FULLAUTO,
        BURST,
        SINGLE
    }
    [SerializeField] protected bool canSwitchFireMode;
    [SerializeField] protected FIREMODE fireMode;
    public FIREMODE FireMode => fireMode;

    public virtual void Init(BulletData bulletData)
    {
        this.bulletData = bulletData;
    }
    public virtual FIREMODE  SwitchFireMode(FIREMODE fireMode)
    {
        if(!canSwitchFireMode) return fireMode;
        switch (fireMode)
        {
            case FIREMODE.FULLAUTO:
                return this.fireMode = FIREMODE.SINGLE;
            case FIREMODE.SINGLE:
                return this.fireMode =  FIREMODE.FULLAUTO;
            default:
                return FIREMODE.SINGLE;
        }
    }
    public abstract void Fire(int damage);
}
