using System;
using System.Collections.Generic;
using UnityEngine;
public class PartnerRuntimeSystem : MonoBehaviour
{
    public static PartnerRuntimeSystem Instance{get; private set;}
    public static event Action OnActivePartnerChanged;
    public List<PartnerRuntimeData> partners;
    public PartnerRuntimeData currentPartner;

    void Awake()
    {
        if(Instance != null && Instance != this)
        {
            Destroy(gameObject);
            return;
        }
        Instance = this;
    }
    public PartnerRuntimeData GetPartner(PartnerData data)
    {
        return partners.Find(p => p.data == data);
    }
    public void SetActivePartner(PartnerData data)
    {
        currentPartner = GetPartner(data);
        OnActivePartnerChanged?.Invoke();
    }
    public void UnlockPartner(PartnerData data)
    {
        var partner = GetPartner(data);
        if (partner != null)
            partner.unlocked = true;
            OnActivePartnerChanged?.Invoke();
    }

}


[System.Serializable]
public class PartnerRuntimeData
{
    public PartnerData data;
    public int level;
    public bool unlocked;
    private const int MAX_LEVEL = 10;

    public int GetAttack()
    {
        return Mathf.CeilToInt(data.baseAttack * (1 + (level * 0.1f)));
    }

    public float GetFireRate()
    {
        return  (1 + ((data.baseFireRate + level) * 0.1f));
    }

    public float GetReloadSpeed() 
    {
        return data.baseReloadSpeed * (1 + level * 0.1f);
    }

    public float GetPunchback()
    {
        return data.basePunchback + level * 0.5f;
    }

}
