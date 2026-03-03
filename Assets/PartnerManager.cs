using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PartnerManager : MonoBehaviour
{
    [SerializeField] PartnerRuntimeSystem runtimeSystem;
    [SerializeField] GameObject partnerPrefab;
    [SerializeField] Transform partnerSpawner;

    void OnEnable()
    {
        GameController.OnGameStart += SpawnPartner;
    }
    void OnDisable()
    {
        GameController.OnGameStart -= SpawnPartner;
    }

    public void SpawnPartner()
    {
        PartnerRuntimeData runtimeData = runtimeSystem.currentPartner;
        if(runtimeData == null || runtimeData.data == null) return;
        if(partnerPrefab != null)
        {
            Destroy(partnerPrefab);
        }

        partnerPrefab = Instantiate(runtimeData.data.partnerCharacterPrefab);
        partnerPrefab.transform.position = partnerSpawner.transform.position;
        var controller = partnerPrefab.GetComponent<PartnerController>();
        var weaponController = partnerPrefab.GetComponent<PartnerWeaponController>();
        controller.Init(runtimeData);
        weaponController.Init(runtimeData);
    }
}
