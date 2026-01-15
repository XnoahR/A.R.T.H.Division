using UnityEngine;
using TMPro;
using System;
public class DayEndedUI: MonoBehaviour
{
    [SerializeField] private TextMeshProUGUI incomeText;
    [SerializeField] private TextMeshProUGUI totalMoneyText;
    public TextMeshProUGUI TotalMoneyText => totalMoneyText;

    public void UpdateDayEndedUI(int income, int totalMoney)
    {
        incomeText.text = $"Income : {income.ToString()}";
        totalMoneyText.text = $"Total Money: {totalMoney.ToString()}";
    }
}