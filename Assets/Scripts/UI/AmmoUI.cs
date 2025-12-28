using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
public class AmmoUI : MonoBehaviour
{
    [SerializeField] private TextMeshProUGUI ammoText;
    // Start is called before the first frame update
    public void UpdateAmmoUI(int current, int max)
    {
        ammoText.text = $"{current}/{max}";
    }
}
