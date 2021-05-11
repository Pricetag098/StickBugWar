using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UnitSpawner : MonoBehaviour
{
    public GameObject unitDefault;
    public Vector3 enemyBaseLocation;
    public GameObject moneyText;
    public string team = "A";


    currency Currency;

    private void Start()
    {
       Currency = moneyText.GetComponent<currency>();
    }
    public void Miner()
    { 
        
        GameObject unitInstance;
        unitInstance = Instantiate(unitDefault, enemyBaseLocation, Quaternion.identity) as GameObject;
        unitInstance.GetComponent<UnitBrain>().Innit(team,UnitBrain.Classes.miner);
        unitInstance.GetComponent<UnitBrain>().onOrder(new Vector2(-40, Random.Range(-10f, 10f)));
        Currency.subtractMoney(50);
    }
    public void Scout()
    {
        GameObject unitInstance;
        unitInstance = Instantiate(unitDefault, enemyBaseLocation, Quaternion.identity) as GameObject;
        unitInstance.GetComponent<UnitBrain>().Innit(team,UnitBrain.Classes.scout);
        unitInstance.GetComponent<UnitBrain>().onOrder(new Vector2(-40, Random.Range(-10f, 10f)));
        moneyText.GetComponent<currency>().subtractMoney(75);
    }
    public void Archer()
    {
        GameObject unitInstance;
        unitInstance = Instantiate(unitDefault, enemyBaseLocation, Quaternion.identity) as GameObject;
        unitInstance.GetComponent<UnitBrain>().Innit(team,UnitBrain.Classes.archer);
        unitInstance.GetComponent<UnitBrain>().onOrder(new Vector2(-40, Random.Range(-10f, 10f)));
        moneyText.GetComponent<currency>().subtractMoney(150);
    }
    public void Knight()
    {
        GameObject unitInstance;
        unitInstance = Instantiate(unitDefault, enemyBaseLocation, Quaternion.identity) as GameObject;
        unitInstance.GetComponent<UnitBrain>().Innit(team,UnitBrain.Classes.knight);
        unitInstance.GetComponent<UnitBrain>().onOrder(new Vector2(-40, Random.Range(-10f, 10f)));
        moneyText.GetComponent<currency>().subtractMoney(150);
    }
    public void Tank()
    {
        GameObject unitInstance;
        unitInstance = Instantiate(unitDefault, enemyBaseLocation, Quaternion.identity) as GameObject;
        unitInstance.GetComponent<UnitBrain>().Innit(team,UnitBrain.Classes.tank);
        unitInstance.GetComponent<UnitBrain>().onOrder(new Vector2(-40, Random.Range(-10f, 10f)));
        moneyText.GetComponent<currency>().subtractMoney(300);   
    }
    public void Giant()
    {
        GameObject unitInstance;
        unitInstance = Instantiate(unitDefault, enemyBaseLocation, Quaternion.identity) as GameObject;
        unitInstance.GetComponent<UnitBrain>().Innit(team,UnitBrain.Classes.giant);
        unitInstance.GetComponent<UnitBrain>().onOrder(new Vector2(-40, Random.Range(-10f, 10f)));
        moneyText.GetComponent<currency>().subtractMoney(700);
    }

    // Update is called once per frame
    void Update()
    {
        if(Input.GetKeyDown("1"))
        {
            Miner();
        }
        if(Input.GetKeyDown("2"))
        {
            Scout();
        }
        if(Input.GetKeyDown("3"))
        {
            Archer();
        }
        if(Input.GetKeyDown("4"))
        {
            Knight();
        }
        if(Input.GetKeyDown("5"))
        {
            Tank();
        }
        if(Input.GetKeyDown("6"))
        {
            Giant();
        }    
    }
}
