using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class currency : MonoBehaviour
{
    public int money;
    // Start is called before the first frame update
    void Start()
    {
        money = 100;
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void addMoney(int moneyToAdd)
    {
        money += moneyToAdd;
    }

    public void subtractMoney(int moneyToSubtract)
    {
        if(money - moneyToSubtract < 0)
        {
            Debug.Log("Balance too low");
        }
        else
        {
            money -= moneyToSubtract;
        }
    }
}