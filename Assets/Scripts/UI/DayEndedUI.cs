using UnityEngine;
using TMPro;
using System;
public class DayEndedUI: MonoBehaviour
{
    [SerializeField] private TextMeshProUGUI dayText;
    [SerializeField] private TextMeshProUGUI incomeText;
    [SerializeField] private TextMeshProUGUI totalMoneyText;
    public TextMeshProUGUI TotalMoneyText => totalMoneyText;

    public void UpdateDayEndedUI(int income, int totalMoney)
    {
        dayText.text = $"Day {GameController.Instance.GetDay()} Passed";
        incomeText.text = $"Income : {income} Z";
        totalMoneyText.text = $"Total Money: {totalMoney} Z";
    }
}