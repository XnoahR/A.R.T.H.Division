using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum SPECIAL_TYPE
{
    GROUND,
    AIR,
    BOTH
}
[CreateAssetMenu(menuName = "SpecialData")]
public class SpecialData : ScriptableObject
{
    public SPECIAL_TYPE type;
    public string specialName;
    public int specialCooldown;
    [SerializeField] private GameObject specialObj;
    public bool needOffset;
    public Vector3 spawnOffset;
    public void Execute(Transform spawnPoint)
    {
        Vector3 spawnPos = spawnPoint.position;
        if (needOffset)
        {
            spawnPos = spawnPoint.position + spawnOffset;
        }
        GameObject specialGO = Instantiate(specialObj, spawnPos, Quaternion.identity);
        specialGO.GetComponent<SpecialBase>().Play();
    }
}