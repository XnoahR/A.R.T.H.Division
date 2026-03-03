using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class UpgradeUI : MonoBehaviour
{
    [SerializeField] private DayController dayController;

    [Header("Buttons")]
    public Button skipButton;
    public Button RefreshButton;
    public Button UnlockButton;
    public Button UpgradeButton;
    public Button ActivateButton;

    [Header("Texts")]
    public TextMeshProUGUI durabilityText;
    public TextMeshProUGUI moneyText;
    public TextMeshProUGUI dayText;
    public TextMeshProUGUI cardDescriptionText;
    public Image gunSprite;

    [Header("Containers")]
    [SerializeField] GameObject partnerContainerUI;
    [SerializeField] GameObject upgradeContainerUI;
    [SerializeField] GameObject partnerButtons;
    public CanvasGroup upgradeUIGroup;
    public GameObject P1;
    public GameObject P2;


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
        PartnerUpgradeController.OnCardChoosed += UpdatePartnerButtons;
    }
    void OnDisable()
    {
        UpgradeController.OnUpgraded -= DisableButton;
        WeaponController.onWeaponChange -= UpdateGunData;
        WeaponController.OnDurabilityChange -= UpdateDurabilityUI;
        PlayerEconomy.OnMoneyChanged -= UpdateMoneyText;
        UpgradeCard.OnCardHover -= UpdateCardDescription;
        UpgradeCard.OnCardExit -= UpdateCardDescription;
        PartnerUpgradeController.OnCardChoosed -= UpdatePartnerButtons;
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
        partnerContainerUI.SetActive(false);
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
        cardDescriptionText.text = ud == null ? "" : ud.description;
    }
    public void EnableButton()
    {
        upgradeUIGroup.interactable = true;
    }


    public void ChangeTab(GameObject tabButton)
    {
        if (tabButton == null) return;
        if (tabButton.name == "UpgradeTab")
        {
            upgradeContainerUI.SetActive(true);
            partnerContainerUI.SetActive(false);
            RefreshButton.gameObject.SetActive(true);
            partnerButtons.SetActive(false);
        }
        else if (tabButton.name == "PartnerTab")
        {
            partnerContainerUI.SetActive(true);
            upgradeContainerUI.SetActive(false);
            RefreshButton.gameObject.SetActive(false);
            partnerButtons.SetActive(true);
            ActivateButton.gameObject.SetActive(false);
            UpgradeButton.gameObject.SetActive(false);
            UnlockButton.gameObject.SetActive(false);
        }
    }

    public void UpdatePartnerButtons(PartnerRuntimeData partnerRuntimeData)
    {
        if (partnerRuntimeData == null)
        {
            ActivateButton.gameObject.SetActive(false);
            UpgradeButton.gameObject.SetActive(false);
            UnlockButton.gameObject.SetActive(false);
            return;
        }

        UnlockButton.gameObject.SetActive(!partnerRuntimeData.unlocked);
        ActivateButton.gameObject.SetActive(partnerRuntimeData.unlocked);
        UpgradeButton.gameObject.SetActive(partnerRuntimeData.unlocked);
    }
    public void DisableButton()
    {
        upgradeUIGroup.interactable = false;
    }
}
