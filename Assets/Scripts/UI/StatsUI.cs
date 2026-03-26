using System.Collections.Generic;
using UnityEngine;

public class StatsUI : MonoBehaviour
{
    [SerializeField] private StatBarUI[] statBars;
    [SerializeField] private PlayerController player;

    private Dictionary<STAT_TYPE, StatBarUI> barLookup;

    void Awake()
    {
        barLookup = new Dictionary<STAT_TYPE, StatBarUI>();
        foreach (var bar in statBars)
        {
            barLookup.Add(bar.StatType, bar);
        }
    }

    void OnEnable()
    {
        UpgradeController.OnStatsUpgraded += UpdateStat;
        EarlyBarUpdate();
    }


    void OnDisable()
    {
        UpgradeController.OnStatsUpgraded -= UpdateStat;
    }
    void EarlyBarUpdate()
    {
        PlayerData data = GameController.Instance.playerData;
        UpdateBar(STAT_TYPE.ATTACK, data.attackLevel);
        UpdateBar(STAT_TYPE.FIRE_RATE, data.fireRateLevel);
        UpdateBar(STAT_TYPE.MAGAZINE_CAPACITY, data.magazineCapacityLevel);
        UpdateBar(STAT_TYPE.RELOAD_SPEED, data.reloadSpeedLevel);
        UpdateBar(STAT_TYPE.PUNCHBACK, data.punchbackLevel);
    }
    void UpdateStat(STAT_TYPE type, int level)
    {
        UpdateBar(type, level);
    }
    public void UpdateBar(STAT_TYPE type, int level)
    {
        if (barLookup.TryGetValue(type, out var bar))
        {
            bar.UpdateBar(level);
        }
    }
}
