using UnityEngine;

[CreateAssetMenu(menuName = "Upgrade/Stats Upgrade")]
public class StatsUpgradeData : UpgradeData
{
    public int attackValue;
    public int magazineCapacityValue;
    public int reloadSpeedValue;
    public int fireRateValue;
    public int punchbackValue;

    public override void Apply(GameObject target)
    {
        target.GetComponent<PlayerController>()?.AddStats(this);
    }
}