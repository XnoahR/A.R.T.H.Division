using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class UpgradeUI : MonoBehaviour
{
    [SerializeField] private DayController dayController;
    public GameObject P1;
    public GameObject P2;
    public Button skipButton;
    public Button RefreshButton;
    public TextMeshProUGUI durabilityText;
    public TextMeshProUGUI moneyText;
    public TextMeshProUGUI dayText;
    public TextMeshProUGUI cardDescriptionText;
    public Image gunSprite;
    public void OnEnable()
    {
        P1.gameObject.SetActive(true);
        P2.gameObject.SetActive(false);
        UpgradeController.OnUpgraded += DisableButton;
        WeaponController.onWeaponChange += UpdateGunData;
        WeaponController.OnDurabilityChange += UpdateDurabilityUI;
        PlayerEconomy.OnMoneyChanged += UpdateMoneyText;
        UpgradeCard.OnCardHover += UpdateCardDescription;
        UpgradeCard.OnCardExit += UpdateCardDescription;
    }
    void OnDisable()
    {
        UpgradeController.OnUpgraded -= DisableButton;
        WeaponController.onWeaponChange -= UpdateGunData;
        WeaponController.OnDurabilityChange -= UpdateDurabilityUI;
        PlayerEconomy.OnMoneyChanged -= UpdateMoneyText;
    }

    void RefreshWeaponUI()
    {
        WeaponController wc = FindObjectOfType<WeaponController>();
        if (wc == null) return;

        UpdateGunData(wc.gunData);
        UpdateMoneyText(PlayerEconomy.Instance.Money);
        UpdateDurabilityUI(wc.IsPermanent, wc.CurrentWeaponDurability);
    }

    public void NextButton()
    {
        P1.gameObject.SetActive(false);
        dayController.UpgradePage();
        P2.gameObject.SetActive(true);
        dayText.text = $"Day : {dayController.currentDay}";
        EnableButton();
        RefreshWeaponUI();
    }

    public void UpdateGunData(GunData gunData)
    {
        gunSprite.sprite = gunData.gunSprite;
    }

    private void UpdateMoneyText(int money)
    {
        moneyText.text = $"Money : {money}";
    }

    private void UpdateDurabilityUI(bool isPermanent, int currentDurability)
    {
        Debug.Log("Permanent: " + isPermanent);
        durabilityText.text = isPermanent
            ? "Durability : -"
            : $"Durability : {currentDurability}";
    }

    private void UpdateCardDescription(UpgradeData ud)
    {
       cardDescriptionText.text = ud == null? "" : ud.description; 
    }
    public void EnableButton()
    {
        skipButton.interactable = true;
        RefreshButton.interactable = true;
    }
    public void DisableButton()
    {
        skipButton.interactable = false;
        RefreshButton.interactable = false;
    }
}
