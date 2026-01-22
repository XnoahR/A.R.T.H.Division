using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ObjectiveController : MonoBehaviour, IDamageable
{
    [SerializeField] private int health;
    [SerializeField] private int maxHealth = 100;
    public static event Action<int> OnObjectiveHealthChanged;
    bool isGameOver;
    // Start is called before the first frame update
    void Awake()
    {
        health = maxHealth;
        isGameOver = false;
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.J))
        {
            RegenHealth(25);
        }
    }

    void OnEnable()
    {
        GameController.OnGameRestart += ResetObjective;
    }

    void OnDisable()
    {
        GameController.OnGameRestart -= ResetObjective;
    }
    public void TakeDamage(int damage)
    {
        if (isGameOver) return;
        health -= damage;
        OnObjectiveHealthChanged?.Invoke(health);
        if (health <= 0)
        {
            isGameOver = true;
            GameOver();
        }
    }
    public void RegenHealth(int regen)
    {
        health += regen;
        if (health > 100) health = 100;
        OnObjectiveHealthChanged?.Invoke(health);

    }

    public void GameOver()
    {
        GameController.Instance.SetState(Core.Game.GAME_STATE.GAMEOVER);
    }



    public void ResetObjective()
    {
        health = maxHealth;
        isGameOver = false;
        OnObjectiveHealthChanged?.Invoke(health);
    }
}
