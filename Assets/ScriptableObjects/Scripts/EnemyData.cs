using System.Collections;
using System.Collections.Generic;
using UnityEngine;

enum TARGET
{
     PLAYER, //2 HP?
     OBJECTIVE
}

[CreateAssetMenu(menuName = "EnemyData")]
public class EnemyData : ScriptableObject
{
     public string enemyName;
     public int health;
     public int MAX_HEALTH;
     public int speed;
     public int damage;
     public int attackSpeed;
     public bool isFly;

}