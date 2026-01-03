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
        player.OnStatChanged += UpdateStat;


        UpdateBar(STAT_TYPE.ATTACK, player.AttackLevel);
        UpdateBar(STAT_TYPE.FIRE_RATE, player.FireRateLevel);
        UpdateBar(STAT_TYPE.MAGAZINE_CAPACITY, player.MagazineCapacityLevel);
        UpdateBar(STAT_TYPE.RELOAD_SPEED, player.ReloadSpeedLevel);


    }

    void OnDisable()
    {
        player.OnStatChanged -= UpdateStat;
    }

    // INI YANG KAMU TANYA "hah?"
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
