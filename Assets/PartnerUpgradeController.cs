using System.Collections;
using System;
using System.Collections.Generic;
using UnityEngine;

public class PartnerUpgradeController : MonoBehaviour
{

    public List<PartnerCard> partnersCards;
    public static event Action<PartnerRuntimeData> OnCardChoosed;
    [SerializeField] PartnerData CurrentPartner;
    [SerializeField] PartnerRuntimeData ChoosedCard;
    [SerializeField] GameObject player;
    public int partnerLimits = 5;


    void OnEnable()
    {
        PartnerRuntimeSystem.OnActivePartnerChanged += LoadPartner;
        GameController.OnGameUpgrade += LoadPartner;
        LoadPartner();
    }
    void OnDisable()
    {
        PartnerRuntimeSystem.OnActivePartnerChanged -= LoadPartner;
        GameController.OnGameUpgrade -= LoadPartner;
    }
    void LoadPartner()
    {

        for (int i = 0; i < PartnerRuntimeSystem.Instance.partners.Count; i++)
        {
            partnersCards[i].SetPartnerRuntimeData(PartnerRuntimeSystem.Instance.partners[i]);
            partnersCards[i].SetupCard();
        }
        ChoosedCard = null;
        OnCardChoosed?.Invoke(null);
    }

    public void ChoosePartner(PartnerRuntimeData partnerRuntimeData)
    {
        ChoosedCard = partnerRuntimeData;
        OnCardChoosed?.Invoke(ChoosedCard);
        Debug.Log($"Current Partner {ChoosedCard}");
    }

    public void UnlockPartner()
    {
        if (ChoosedCard == null || ChoosedCard.unlocked)
        {
            return;
        }
        var economy = player.GetComponent<PlayerEconomy>();

        if (economy.Money < ChoosedCard.data.unlockCost)
        {
            Debug.Log("No money for Unlock partner");
            Debug.Log($"Money: {economy.Money}, Unlock cost: {ChoosedCard.data.unlockCost}");
            return;
        }
        economy.SpendMoney(ChoosedCard.data.unlockCost);
        PartnerRuntimeSystem.Instance.UnlockPartner(ChoosedCard.data);
    }
    public void ActivatePartner()
    {
        if (ChoosedCard == null || PartnerRuntimeSystem.Instance.currentPartner == ChoosedCard)
        {
            return;
        }

        PartnerRuntimeSystem.Instance.SetActivePartner(ChoosedCard.data);
    }
}
