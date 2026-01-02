using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Core.Game;
using TMPro;

public class UpgradeController : MonoBehaviour
{
    [SerializeField] GameObject player;
    [SerializeField] GameController gameController;
    [SerializeField] UpgradeData upgradeData;
    [SerializeField] TextMeshProUGUI upgradeNameText;

    void OnEnable()
    {
        upgradeNameText.text = upgradeData.upgradeName;
    }
    
    public void Choose()
    {
        if(player.GetComponent<PlayerEconomy>().money < upgradeData.cost)
        {
            Debug.Log("No money!");
            return;
        }
        upgradeData.Apply(player);
        gameController.SetState(GAME_STATE.DAYSTART);

    }
}
