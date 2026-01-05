using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerEconomy : MonoBehaviour
{
    public int money;
    // Start is called before the first frame update
    
    

    public int AddMoney(int amount)
    {
        money += amount;
        Debug.Log("Money Added");
        return amount;
    }
    
    public int GetMoney()
    {
        return money;
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
