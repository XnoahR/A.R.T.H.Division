using System;

[Serializable]
public class PlayerData
{
    public int health = 5;
    public int attackLevel = 1;
    public int fireRateLevel = 1;
    public int reloadSpeedLevel = 1;
    public int magazineCapacityLevel = 1;
    public int punchbackLevel = 1;

    public GunData currentWeapon;
}