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
    public Image gunSprite;
    public void OnEnable()
    {
        P1.gameObject.SetActive(true);
        P2.gameObject.SetActive(false);
        UpgradeController.OnUpgraded += DisableButton;
        WeaponController.onWeaponChange += UpdateGunData;
        WeaponController.OnDurabilityChange += UpdateDurabilityUI;
    }
    void OnDisable()
    {
        UpgradeController.OnUpgraded -= DisableButton;
        WeaponController.onWeaponChange -= UpdateGunData;
        WeaponController.OnDurabilityChange -= UpdateDurabilityUI;
    }

    void RefreshWeaponUI()
    {
        WeaponController wc = FindObjectOfType<WeaponController>();
        if (wc == null) return;

        UpdateGunData(wc.gunData);
        UpdateDurabilityUI(wc.IsPermanent, wc.CurrentWeaponDurability);
    }

    public void NextButton()
    {
        P1.gameObject.SetActive(false);
        dayController.UpgradePage();
        P2.gameObject.SetActive(true);
        EnableButton();
        RefreshWeaponUI();
    }

    public void UpdateGunData(GunData gunData)
    {
        gunSprite.sprite = gunData.gunSprite;
    }

    private void UpdateDurabilityUI(bool isPermanent, int currentDurability)
    {
        Debug.Log("Permanent: " + isPermanent);
        durabilityText.text = isPermanent
            ? "Durability : -"
            : $"Durability : {currentDurability}";
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
