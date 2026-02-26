using System.Collections;
using System.Collections.Generic;
using UnityEngine;




[CreateAssetMenu(menuName = "Game/Ability/Self Ability")]
public class SelfAbilityData : AbilityData
{
    [SerializeField] private GameObject selfAbilityObj;
    public override void Execute(PlayerController playerController)
    {
        selfAbilityObj.GetComponent<AbilityBase>().Apply(playerController);
    }
}