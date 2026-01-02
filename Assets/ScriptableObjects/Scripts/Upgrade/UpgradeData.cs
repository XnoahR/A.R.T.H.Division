using UnityEngine;

public enum UPGRADE_CATEGORY
{
   STATS,
   WEAPON,
   OBJECTIVE
}


[CreateAssetMenu(menuName = "Upgrade/Upgrade Data")]
public class UpgradeData : ScriptableObject
{
  public string upgradeName;
    [TextArea] public string description;
    public Sprite icon;

    public UPGRADE_CATEGORY category;
    public int cost;

    public virtual void Apply(GameObject target) { }
}
