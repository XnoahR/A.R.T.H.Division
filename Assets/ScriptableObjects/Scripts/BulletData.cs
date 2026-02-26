using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "Game/Bullet")]
public class BulletData : ScriptableObject
{
    public int additionalDamage;
    public float bulletSpeed;
    public GameObject BulletGO; //Or perhaps just use ParticleSystem

}