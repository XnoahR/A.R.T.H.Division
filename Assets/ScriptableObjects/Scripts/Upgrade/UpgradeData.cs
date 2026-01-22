using UnityEngine;

public enum UPGRADE_CATEGORY
{
   STATS,
   WEAPON,
   OBJECTIVE
}

public enum STAT_TYPE
{
  ATTACK,
  FIRE_RATE,
  MAGAZINE_CAPACITY,
  RELOAD_SPEED,
  PUNCHBACK
}

[CreateAssetMenu(menuName = "Upgrade/Upgrade Data")]
public class UpgradeData : ScriptableObject
{
  public string upgradeName;
    [TextArea] public string description;
    public Sprite icon;

    public UPGRADE_CATEGORY category;
    public STAT_TYPE type;
    public int cost;

    public virtual void Apply(GameObject target) { }
}
