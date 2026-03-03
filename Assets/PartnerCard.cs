using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;



public class PartnerCard : MonoBehaviour
{
    public PartnerUpgradeController partnerUpgradeController;
    public PartnerRuntimeData partnerRuntimeData;
    public Image partnerImage;
    public TextMeshProUGUI partnerName;
    public TextMeshProUGUI partnerWeapon;
    public TextMeshProUGUI partnerLevel;
    [SerializeField] GameObject lockedPanel;

    public void SetupCard()
    {
        // Debug.Log("Joe");
        partnerImage.sprite = partnerRuntimeData.data.partnerSprite;
        partnerName.text = partnerRuntimeData.data.partnerName;
        partnerWeapon.text = partnerRuntimeData.data.partnerWeapon;
        partnerLevel.text = $"Level {partnerRuntimeData.level}";
        lockedPanel.gameObject.SetActive(!partnerRuntimeData.unlocked);
    }

    public void SetPartnerRuntimeData(PartnerRuntimeData partnerRuntimeData)
    {
        this.partnerRuntimeData = partnerRuntimeData;
    }

    public void OnClick()
    {
        partnerUpgradeController.ChoosePartner(partnerRuntimeData);
        Debug.Log("Clicked partner");
    }
}
