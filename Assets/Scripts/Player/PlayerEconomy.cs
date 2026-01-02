using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerEconomy : MonoBehaviour
{
    public int money;
    // Start is called before the first frame update
    void OnEnable()
    {
        DayController.OnDayEnded += AddMoney;
    }
    void OnDisable()
    {
        DayController.OnDayEnded -= AddMoney;
    }
    

    void AddMoney()
    {
        money += 5;
        Debug.Log("Money Added");
    }
    
    public void SpendMoney(int amount)
    {
        if(money < amount)
        {
            Debug.Log("Money not enough!");
        }
        money -= amount;
    }
}
