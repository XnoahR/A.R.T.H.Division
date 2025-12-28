using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "EnemyData")]
public class EnemyData : ScriptableObject
{
     public int health;
     public int MAX_HEALTH;
     public int speed;
     public int damage;
     public bool isFly;

}