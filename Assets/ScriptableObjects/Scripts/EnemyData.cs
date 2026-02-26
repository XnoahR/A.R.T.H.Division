using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum TARGET
{
    PLAYER,
    OBJECTIVE
}

[CreateAssetMenu(menuName = "Game/Enemy")]
public class EnemyData : ScriptableObject
{
    public string enemyName;
    public int health;
    public int MAX_HEALTH;
    public int speed;
    public int damage;
    public int attackSpeed;
    public bool isFly;
    public TARGET attackTarget;
    public float knockbackResistance;

    [Header("Optional")]
    public float attackRange = 1f; // Jarak maksimal untuk menyerang
}
