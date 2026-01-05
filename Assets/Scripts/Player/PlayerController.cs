using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting.Antlr3.Runtime.Tree;
using UnityEngine;
using Core.Game;
using System;
public class PlayerController : MonoBehaviour
{
    [Header("Stats")]
    [SerializeField] float speed;
    public int FireRateLevel;
    public int MagazineCapacityLevel;
    public int AttackLevel;
    public int ReloadSpeedLevel;

    public bool isRight = true;
    public bool canPlay = true;
    public event Action<STAT_TYPE, int> OnStatChanged;
    // Start is called before the first frame update
    void OnEnable()
    {
        GameController.OnGamePaused += ChangePlayState;
    }
    void OnDisable()
    {
        GameController.OnGamePaused += ChangePlayState;
    }

    void Awake()
    {
        FireRateLevel = 10; 
        MagazineCapacityLevel = 20;
        AttackLevel = 1;
        ReloadSpeedLevel = 1;
    }


    // Update is called once per frame
    void FixedUpdate()
    {
        //movement
        if (canPlay) { Move(); }

    }

    private void Move()
    {
        float horizontalInput = Input.GetAxisRaw("Horizontal");
        transform.Translate(Vector2.right * horizontalInput * Time.fixedDeltaTime * speed);
    }

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.CapsLock))
        {
            canPlay = !canPlay;
        }
    }

    public void AddStats(StatsUpgradeData upgradeData)
    {
        if (upgradeData.attackValue != 0)
        {
            AttackLevel += upgradeData.attackValue;
            OnStatChanged?.Invoke(STAT_TYPE.ATTACK, AttackLevel);
        }

        if (upgradeData.fireRateValue != 0)
        {
            FireRateLevel += upgradeData.fireRateValue;
            OnStatChanged?.Invoke(STAT_TYPE.FIRE_RATE, FireRateLevel);
        }

        if (upgradeData.magazineCapacityValue != 0)
        {
            MagazineCapacityLevel += upgradeData.magazineCapacityValue;
            OnStatChanged?.Invoke(STAT_TYPE.MAGAZINE_CAPACITY, MagazineCapacityLevel);
        }

        if (upgradeData.reloadSpeedValue != 0)
        {
            ReloadSpeedLevel += upgradeData.reloadSpeedValue;
            OnStatChanged?.Invoke(STAT_TYPE.RELOAD_SPEED, ReloadSpeedLevel);
        }
    }

    private void ChangePlayState(GAME_STATE state)
    {
        canPlay = state == GAME_STATE.PLAY || state == GAME_STATE.DAYSTART;
        Debug.Log(canPlay);
    }

    public void Flip(Transform weaponPivot)
    {
        isRight = !isRight;
        Vector3 aScale = weaponPivot.transform.localScale;
        aScale.x *= -1;
        aScale.y *= -1;
        weaponPivot.transform.localScale = aScale;
        Vector3 scale = transform.localScale;
        scale.x *= -1;
        transform.localScale = scale;


        // Vector3 wScale = currentWeapon.transform.localScale;
        // wScale.x *= -1;
        // wScale.y *= -1;
        // currentWeapon.transform.localScale = wScale;
    }
}
