using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "BulletData")]
public class BulletData : ScriptableObject
{
    public int additionalDamage;
    public float bulletSpeed;
    public GameObject BulletGO; //Or perhaps just use ParticleSystem

}