using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UpgradeUI : MonoBehaviour
{
    public GameObject P1;
    public GameObject P2;

    public void OnEnable()
    {
        P1.gameObject.SetActive(true);
        P2.gameObject.SetActive(false);
    }
    public void NextButton()
    {
        P1.gameObject.SetActive(false);
        P2.gameObject.SetActive(true);
    }
}
