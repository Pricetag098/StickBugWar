using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class currency : MonoBehaviour
{
    public int money;
    public Text moneyText;
    // Start is called before the first frame update
    void Start()
    {
        moneyText = GetComponent<Text>();
        moneyText.text = "$" + money;
    }

    // Update is called once per frame
    void Update()
    {
    }

    public void addMoney(int moneyToAdd)
    {
        money += moneyToAdd;
        moneyText.text = "$" + money;
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
            moneyText.text = "$" + money;
        }
    }
}