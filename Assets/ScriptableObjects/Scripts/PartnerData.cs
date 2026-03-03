using UnityEngine;

[CreateAssetMenu(menuName = "Game/Partner Data")]
public class PartnerData : ScriptableObject
{
    public GameObject partnerCharacterPrefab;
    public GunData gunData;
    public int baseFireRate;
    public int baseMagazineCapacity;
    public int baseAttack;
    public int baseReloadSpeed;
    public int basePunchback;

    public string partnerName;
    public string partnerWeapon;
    public Sprite partnerSprite;
    public int unlockCost;

}