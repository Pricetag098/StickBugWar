using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UnitSpawner : MonoBehaviour
{
    public GameObject unitDefault;
    public Vector3 enemyBaseLocation;
    public GameObject moneyText;
    public string team = "A";

    public void Miner()
    {   GameObject unitInstance;
            unitInstance = Instantiate(unitDefault, enemyBaseLocation, Quaternion.identity) as GameObject;
            unitInstance.GetComponent<UnitBrain>().Innit(team,UnitBrain.Classes.scout);
    }
    public void Scout()
    {
        GameObject unitInstance;
            unitInstance = Instantiate(unitDefault, enemyBaseLocation, Quaternion.identity) as GameObject;
            unitInstance.GetComponent<UnitBrain>().Innit(team,UnitBrain.Classes.scout);
    }
    public void Archer()
    {
        GameObject unitInstance;
            unitInstance = Instantiate(unitDefault, enemyBaseLocation, Quaternion.identity) as GameObject;
            unitInstance.GetComponent<UnitBrain>().Innit(team,UnitBrain.Classes.archer);
    }
    public void Knight()
    {
        GameObject unitInstance;
            unitInstance = Instantiate(unitDefault, enemyBaseLocation, Quaternion.identity) as GameObject;
            unitInstance.GetComponent<UnitBrain>().Innit(team,UnitBrain.Classes.knight);
    }
    public void Tank()
    {
        GameObject unitInstance;
            unitInstance = Instantiate(unitDefault, enemyBaseLocation, Quaternion.identity) as GameObject;
            unitInstance.GetComponent<UnitBrain>().Innit(team,UnitBrain.Classes.tank);
            unitInstance.GetComponent<UnitBrain>().onOrder(new Vector2(-30f,0f));
        
    }
    public void Giant()
    {
        GameObject unitInstance;
            unitInstance = Instantiate(unitDefault, enemyBaseLocation, Quaternion.identity) as GameObject;
            unitInstance.GetComponent<UnitBrain>().Innit(team,UnitBrain.Classes.giant);
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
        /*if(Input.GetKeyDown("6"))
        {
            Giant();
        }*/       
    }
}
