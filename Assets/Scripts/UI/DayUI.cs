using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
public class DayUI : MonoBehaviour
{
    [SerializeField] private TextMeshProUGUI dayText;
    // Start is called before the first frame update
    public void UpdateDayUI(int currentDay)
    {
        dayText.text = $"Day {currentDay}";
    }
}
