using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using Microlight.MicroBar;
using UnityEngine.UI;
public class UIController : MonoBehaviour
{
    [Header("Gameplay UI")]
    [SerializeField] MicroBar objectiveHealthBar;
    [SerializeField] AmmoUI ammoUI;
    [SerializeField] DayUI dayUI;
    [SerializeField] Image GunSprite;

    [Header("Gameover UI")]
    [SerializeField] GameObject GameOverUI;


    [Header("References")]
    [SerializeField] WeaponController weaponController;
    [SerializeField] DayController dayController;
    [SerializeField] DayEndedUI dayEndedUI;
    [SerializeField] GameObject UpgradeUI;
    // Start is called before the first frame update

    void Start()
    {
        objectiveHealthBar.Initialize(100);
    }
    void OnEnable()
    {
        DayController.OnDayEnded += UpgradeMode;
        DayController.OnGameEnd += dayEndedUI.UpdateDayEndedUI;
        GameController.OnGameStart += GameMode;
        weaponController.OnAmmoChange += ammoUI.UpdateAmmoUI;
        WeaponController.onWeaponChange += UpdateWeaponSprite;
        dayController.OnDayChanged += dayUI.UpdateDayUI;
        ObjectiveController.OnObjectiveHealthChanged += UpdateObjectiveHealth;
        GameController.OnGameOver += GameOverScreen;
        ammoUI.UpdateAmmoUI(weaponController.CurrentAmmo, weaponController.MagazineCapacity);
        dayUI.UpdateDayUI(dayController.currentDay);
    }

    void OnDisable()
    {
        DayController.OnDayEnded -= UpgradeMode;
        DayController.OnGameEnd -= dayEndedUI.UpdateDayEndedUI;
        GameController.OnGameStart -= GameMode;
        weaponController.OnAmmoChange -= ammoUI.UpdateAmmoUI;
        WeaponController.onWeaponChange -= UpdateWeaponSprite;
        GameController.OnGameOver -= GameOverScreen;
        ObjectiveController.OnObjectiveHealthChanged -= UpdateObjectiveHealth;
        dayController.OnDayChanged -= dayUI.UpdateDayUI;
    }
    void GameMode()
    {
        UpgradeUI.SetActive(false);
    }
    void UpgradeMode()
    {
        UpgradeUI.SetActive(true);
    }
    void UpdateWeaponSprite(GunData gunData)
    {
        GunSprite.sprite = gunData.gunSprite;
    }
    void UpdateObjectiveHealth(int health)
    {
        objectiveHealthBar.UpdateBar(health);
    }

    void GameOverScreen(bool status)
    {
        GameOverUI.SetActive(status);
    }

    public void Restart()
    {
        GameController.Instance.RestartGame();
        GameOverScreen(false);
    }
    // Update is called once per frame
    void Update()
    {

    }
}
