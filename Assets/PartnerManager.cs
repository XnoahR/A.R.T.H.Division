using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PartnerManager : MonoBehaviour
{
    [SerializeField] GameObject currentPartnerObj;
    [SerializeField] Transform partnerSpawner;
    public void SpawnPartner(PartnerRuntimeData runtimeData)
    {
        if(runtimeData == null) return;
        if(currentPartnerObj != null)
        {
            Destroy(currentPartnerObj);
        }

        currentPartnerObj = Instantiate(runtimeData.data.partnerCharacterPrefab);
        currentPartnerObj.transform.position = partnerSpawner.transform.position;
        var controller = currentPartnerObj.GetComponent<PartnerController>();
        var weaponController = currentPartnerObj.GetComponent<PartnerWeaponController>();
        controller.Init(runtimeData);
        weaponController.Init(runtimeData);
    }
}
