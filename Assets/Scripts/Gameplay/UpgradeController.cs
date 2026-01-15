using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Core.Game;
using TMPro;
using System;

public class UpgradeController : MonoBehaviour
{
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

    public void Choose(UpgradeData data)
    {
        var economy = player.GetComponent<PlayerEconomy>();
        if (economy.Money < data.cost)
        {
            Debug.Log("Not enough money");
            return;
        }

        economy.SpendMoney(data.cost);
        data.Apply(player);
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

    IEnumerator DayStart()
    {
        yield return new WaitForSeconds(2);

        upgradeContainer.Hide();
        gameController.SetState(GAME_STATE.DAYSTART);
    }
}
