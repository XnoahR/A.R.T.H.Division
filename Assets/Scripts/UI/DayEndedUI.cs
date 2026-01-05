using UnityEngine;
using TMPro;
public class DayEndedUI: MonoBehaviour
{
    [SerializeField] private TextMeshProUGUI incomeText;
    [SerializeField] private TextMeshProUGUI totalMoneyText;

    public void UpdateDayEndedUI(int income, int totalMoney)
    {
        incomeText.text = $"Income : {income.ToString()}";
        totalMoneyText.text = $"Total Money: {totalMoney.ToString()}";
    }
}