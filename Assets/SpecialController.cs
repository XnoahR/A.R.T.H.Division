using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpecialController : MonoBehaviour
{
    [SerializeField] SpecialData specialData;
    [SerializeField] PlayerController playerController;
    [SerializeField] Transform spawnPoint;
    [SerializeField] int specialCooldown;
    [SerializeField] float specialCooldownRemaining;
    private bool isCooldown;
    // Start is called before the first frame update
    void Awake()
    {
        playerController = GetComponent<PlayerController>();
        Init();
    }

    void Init()
    {
        specialCooldown = specialData.specialCooldown;
        specialCooldownRemaining = 0;
    }
    // Update is called once per frame
    void Update()
    {
        if (playerController.canPlay)
        {
            if (Input.GetKeyDown(KeyCode.Space) && !isCooldown)
            {
                specialData.Execute(spawnPoint);
                specialCooldownRemaining = specialCooldown;
                isCooldown = true;
            }
            if (isCooldown)
            {
                specialCooldownRemaining -= Time.deltaTime;
                if (specialCooldownRemaining <= 0)
                {
                    isCooldown = false;
                }
            }
        }
    }
}
