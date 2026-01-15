using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Core.Game;
using TMPro;
using UnityEngine.UI;
using System;

public class UpgradeCard : MonoBehaviour
{
    [SerializeField] UpgradeController upgradeController;
    [SerializeField] UpgradeData upgradeData;
    [SerializeField] TextMeshProUGUI upgradeNameText;
    [SerializeField] private Button button;

    public static event Action<UpgradeData> OnCardHover;
    public static event Action<UpgradeData> OnCardExit;
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
        if (!button.interactable) return;
        gameObject.transform.localScale = new Vector3(1.075f, 1.075f, 1.075f);
        OnCardHover?.Invoke(upgradeData);

    }
    public void OnMouseExit()
    {
        if (!button.interactable) return;
        gameObject.transform.localScale = Vector3.one;
        OnCardHover?.Invoke(null);
    }

}
