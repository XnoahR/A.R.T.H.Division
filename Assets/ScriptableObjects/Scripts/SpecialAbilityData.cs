using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum SPECIAL_TYPE
{
    GROUND,
    AIR,
    BOTH
}


[CreateAssetMenu(menuName = "Ability/Special Ability Data")]
public class SpecialAbilityData : AbilityData
{
    [SerializeField] private GameObject specialObj;
    public bool needOffset;
    public Vector3 spawnOffset;
    public override void Execute(PlayerController playerController)
    {
        Vector3 spawnPos = playerController.abilitySpawnPoint.position;
        if (needOffset)
        {
            spawnPos = playerController.abilitySpawnPoint.position + spawnOffset;
        }
        GameObject specialGO = Instantiate(specialObj, spawnPos, Quaternion.identity);
        specialGO.GetComponent<AbilityBase>().Apply(playerController);
    }
}