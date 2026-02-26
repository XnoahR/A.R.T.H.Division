using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName ="Game/Spawner")]
public class SpawnerData : ScriptableObject
{
   public List<GameObject> enemies;
}
