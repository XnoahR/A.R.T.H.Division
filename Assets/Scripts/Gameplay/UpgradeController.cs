using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Core.Game;
using TMPro;
using System;

public class UpgradeController : MonoBehaviour
{
    public static event Action<STAT_TYPE, int> OnStatsUpgraded;
    public static event Action<GunData> OnWeaponUpgraded;
    public static event Action OnUpgraded;
    [Header("References")]
    [SerializeField] List<UpgradeData> upgradeData;
    [SerializeField] UpgradeContainer upgradeContainer;
    [SerializeField] GameObject player;
    [SerializeField] GameController gameController;

    [Header("Settings")]
    [SerializeField] int refreshCost = 2;
    [SerializeField] StatsUI statsUI;

    private List<UpgradeData> currentChoices = new();

    void OnEnable()
    {
        GameController.OnGameUpgrade += Generate;
    }

    void OnDisable()
    {
        GameController.OnGameUpgrade -= Generate;
    }

    public void Generate()
    {
        currentChoices = UpgradeRandomizer.Generate(upgradeData, 3);
        upgradeContainer.Show(currentChoices, this);
    }
    public void PartnerGenerate()
    {

    }

    public static void BroadcastWeaponUpgrade(GunData gunData)
    {
        OnWeaponUpgraded?.Invoke(gunData);
    }
    public void Choose(UpgradeData data)
    {
        var economy = player.GetComponent<PlayerEconomy>();
        if (economy.Money < data.cost)
        {
            Debug.Log("Not enough money");
            return;
        }

        economy.SpendMoney(data.cost);
        data.Apply();
        OnUpgraded?.Invoke();
        StartCoroutine(DayStart());
    }

    public void NoBuy()
    {
        OnUpgraded?.Invoke();
        StartCoroutine(DayStart());
    }

    public void Refresh()
    {
        var economy = player.GetComponent<PlayerEconomy>();

        if (economy.Money < refreshCost)
        {
            Debug.Log("No money for refresh");
            Debug.Log($"Money: {economy.Money}, refresh cost: {refreshCost}");
            return;
        }

        economy.SpendMoney(refreshCost);
        Generate();
    }


    public void AddStats(StatsUpgradeData upgradeData)
    {
        PlayerData data = GameController.Instance.playerData;
        if (upgradeData.attackValue != 0)
        {
            data.attackLevel += upgradeData.attackValue;
            OnStatsUpgraded?.Invoke(STAT_TYPE.ATTACK, data.attackLevel);
        }

        if (upgradeData.fireRateValue != 0)
        {
            data.fireRateLevel += upgradeData.fireRateValue;
            OnStatsUpgraded?.Invoke(STAT_TYPE.FIRE_RATE, data.fireRateLevel);
        }

        if (upgradeData.magazineCapacityValue != 0)
        {
            data.magazineCapacityLevel += upgradeData.magazineCapacityValue;
            OnStatsUpgraded?.Invoke(STAT_TYPE.MAGAZINE_CAPACITY, data.magazineCapacityLevel);
        }

        if (upgradeData.reloadSpeedValue != 0)
        {
            data.reloadSpeedLevel += upgradeData.reloadSpeedValue;
            OnStatsUpgraded?.Invoke(STAT_TYPE.RELOAD_SPEED, data.reloadSpeedLevel);
        }
        if (upgradeData.punchbackValue != 0)
        {
            data.punchbackLevel += upgradeData.punchbackValue;
            OnStatsUpgraded?.Invoke(STAT_TYPE.PUNCHBACK, data.punchbackLevel);
        }
    }

    IEnumerator DayStart()
    {
        yield return new WaitForSeconds(2);

        upgradeContainer.Hide();
        gameController.SetState(GAME_STATE.DAYSTART);
    }
}
