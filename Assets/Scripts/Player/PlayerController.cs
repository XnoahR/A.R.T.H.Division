using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting.Antlr3.Runtime.Tree;
using UnityEngine;
using Core.Game;
using System;

public class PlayerController : MonoBehaviour, IDamageable
{
    [Header("Stats")]
    [SerializeField] float speed;
    public int FireRateLevel;
    public int MagazineCapacityLevel;
    public int AttackLevel;
    public int ReloadSpeedLevel;
    public int punchbackLevel;
    public int health;
    private bool isInvincible;
    private int MAX_HEALTH = 5;
    public bool isRight = true;
    public bool canPlay = true;
    [SerializeField] Transform spawnPoint;
    public Transform abilitySpawnPoint;
    private float facing;
    private float facingDirection;
    private float inputX;
    [SerializeField] Animator animator;
    // Start is called before the first frame update
    void OnEnable()
    {
        GameController.OnGamePaused += ChangePlayState;
        UpgradeController.OnStatsUpgraded += OnStatsUpgraded;
    }
    void OnDisable()
    {
        GameController.OnGamePaused -= ChangePlayState;
        UpgradeController.OnStatsUpgraded -= OnStatsUpgraded;
    }

    void Awake()
    {
        isInvincible = false;
        SetMaxHealth();
        SetPositionSpawn();
    }
    void Start()
    {
        LoadStats();
    }

    void OnStatsUpgraded(STAT_TYPE type, int value)
    {
        LoadStats();
    }
    void LoadStats()
    {
        PlayerData data = GameController.Instance.playerData;

        FireRateLevel = data.fireRateLevel;
        MagazineCapacityLevel = data.magazineCapacityLevel;
        AttackLevel = data.attackLevel;
        ReloadSpeedLevel = data.reloadSpeedLevel;
        punchbackLevel = data.punchbackLevel;

    }
    public void SetMaxHealth()
    {
        health = MAX_HEALTH;
    }
    public void SetPositionSpawn()
    {
        transform.position = spawnPoint.position;
    }

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
        inputX = Input.GetAxisRaw("Horizontal");

        // arah hadap player (bukan input)
        facing = isRight ? 1f : -1f;

        // arah relatif (INI KUNCI)
        facingDirection = inputX * facing;

        // animator
        animator.SetFloat("Speed", Mathf.Abs(inputX));
        animator.SetFloat("Direction", facingDirection);
        if (Input.GetKeyDown(KeyCode.CapsLock))
        {
            canPlay = !canPlay;
        }
    }


    private void ChangePlayState(GAME_STATE state)
    {
        canPlay = state == GAME_STATE.PLAY || state == GAME_STATE.DAYSTART;
    }

    public void TakeDamage(int damage)
    {
        if (isInvincible) return;
        health -= damage;
        StartCoroutine(InvisibilityCooldown());

    }

    public void RegenHealth(int amount)
    {
        if (health >= MAX_HEALTH) return;
        health += amount;
        if (health > MAX_HEALTH) health = MAX_HEALTH;
    }
    IEnumerator InvisibilityCooldown()
    {
        isInvincible = true;
        yield return new WaitForSeconds(5);
        isInvincible = false;
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
