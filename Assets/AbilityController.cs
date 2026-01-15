using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AbilityController : MonoBehaviour
{
    [SerializeField] SpecialAbilityData specialAbilityData;
    [SerializeField] SelfAbilityData selfAbilityData;
    [SerializeField] PlayerController playerController;
    [SerializeField] Transform spawnPoint;
    [SerializeField] int specialCooldown;
    [SerializeField] float specialCooldownRemaining;
    [SerializeField] int selfCooldown;
    [SerializeField] float selfCooldownRemaining;
    private bool isSpecialCooldown;
    private bool isSelfCooldown;
    // Start is called before the first frame update
    void Awake()
    {
        playerController = GetComponent<PlayerController>();
        Init();
    }

    void Init()
    {
        specialCooldown = specialAbilityData.abilityCooldown;
        specialCooldownRemaining = 0;
        selfCooldown = selfAbilityData.abilityCooldown;
        selfCooldownRemaining = 0;
    }
    
    // Update is called once per frame
    void Update()
    {
        if (playerController.canPlay)
        {
            if (Input.GetKeyDown(KeyCode.Q) && !isSpecialCooldown)
            {
                specialAbilityData.Execute(playerController);
                specialCooldownRemaining = specialCooldown;
                isSpecialCooldown = true;
            }
            if (isSpecialCooldown)
            {
                specialCooldownRemaining -= Time.deltaTime;
                if (specialCooldownRemaining <= 0)
                {
                    isSpecialCooldown = false;
                }
            }
            if (Input.GetKeyDown(KeyCode.E) && !isSelfCooldown)
            {
                selfAbilityData.Execute(playerController);
                selfCooldownRemaining = selfCooldown;
                isSelfCooldown = true;
            }
            if (isSelfCooldown)
            {
                selfCooldownRemaining -= Time.deltaTime;
                if (selfCooldownRemaining <= 0)
                {
                    isSelfCooldown = false;
                }
            }
        }
    }
}
