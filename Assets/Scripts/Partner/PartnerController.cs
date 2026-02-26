using UnityEngine;

public class PartnerController : MonoBehaviour
{
    public int FireRate;
    public int MagazineCapacity;
    public int Attack;
    public int ReloadSpeed;
    public int punchback;
    public int currentLevel;

    public void Init(PartnerRuntimeData runtimeData)
    {
        FireRate = runtimeData.data.baseFireRate;
        MagazineCapacity = runtimeData.data.baseMagazineCapacity;
        Attack = runtimeData.data.baseAttack;
        ReloadSpeed = runtimeData.data.baseReloadSpeed;
        punchback = runtimeData.data.basePunchback;
    }


    
}
