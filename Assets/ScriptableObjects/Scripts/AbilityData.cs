using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "Ability/Ability Base")]
public class AbilityData : ScriptableObject
{
    public string abilityName;
    public int abilityCooldown;
 
    public virtual void Execute(PlayerController playerController)
    {
        
    }
}