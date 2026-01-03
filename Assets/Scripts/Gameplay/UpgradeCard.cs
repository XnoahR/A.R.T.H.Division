using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Core.Game;
using TMPro;
using Unity.VisualScripting.Antlr3.Runtime;
using UnityEngine.UI;

public class UpgradeCard : MonoBehaviour
{
    [SerializeField] UpgradeController upgradeController;
    [SerializeField] UpgradeData upgradeData;
    [SerializeField] TextMeshProUGUI upgradeNameText;
    [SerializeField] private Button button;

    void Awake()
    {
        button = GetComponent<Button>();
    }

    public void Setup(UpgradeData data, UpgradeController upgradeController)
    {
        upgradeData = data;
        this.upgradeController = upgradeController;
        upgradeNameText.text = data.upgradeName;
        button.interactable = true;
    }
    void DisableButton()
    {
        button.interactable = false;
    }
    public void OnEnable()
    {
        UpgradeController.OnUpgraded += DisableButton;
    }
    public void OnDisable()
    {
        UpgradeController.OnUpgraded -= DisableButton;
    }
    public void OnClick()
    {
        upgradeController.Choose(upgradeData);
    }

    public void OnMouseEnter()
    {
        gameObject.transform.localScale = new Vector3(1.075f, 1.075f, 1.075f);
    }
    public void OnMouseExit()
    {
        gameObject.transform.localScale = Vector3.one;
    }
    // public void Choose()
    // {
    //     if(player.GetComponent<PlayerEconomy>().money < upgradeData.cost)
    //     {
    //         Debug.Log("No money!");
    //         return;
    //     }
    //     upgradeData.Apply(player);
    //     gameController.SetState(GAME_STATE.DAYSTART);

    // }
}
