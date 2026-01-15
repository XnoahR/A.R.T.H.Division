using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MedkitAbility : AbilityBase
{

    public override void Apply(PlayerController playerController)
    {
        playerController.RegenHealth(3);
    }
}