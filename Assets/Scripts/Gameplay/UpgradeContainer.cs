using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UpgradeContainer : MonoBehaviour
{
    [SerializeField] UpgradeCard[] cards;

    public void Show(List<UpgradeData> data, UpgradeController controller)
    {
        gameObject.SetActive(true);

        for (int i = 0; i < cards.Length; i++)
        {
            cards[i].Setup(data[i], controller);
        }
    }

    public void Hide()
    {
        gameObject.SetActive(false);
    }
}
