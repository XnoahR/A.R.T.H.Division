using System;
using UnityEngine;

public class PlayerEconomy : MonoBehaviour
{
    public static PlayerEconomy Instance;

    public static event Action<int> OnMoneyChanged;

    [SerializeField] private int money;
    public int Money => money;

    private void Awake()
    {
        if (Instance == null) Instance = this;
        else Destroy(gameObject);
    }

    public void AddMoney(int amount)
    {
        money += amount;
        OnMoneyChanged?.Invoke(money);
    }

    public void SpendMoney(int amount)
    {
        if (money < amount)
        {
            Debug.Log("Money not enough!");
            return;
        }

        money -= amount;
        OnMoneyChanged?.Invoke(money);
    }

    public void SetMoney(int amount)
    {
        money = amount;
        OnMoneyChanged?.Invoke(money);
    }
}
