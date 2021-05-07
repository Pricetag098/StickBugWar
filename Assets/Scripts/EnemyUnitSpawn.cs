using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyUnitSpawn : MonoBehaviour
{
    //wave settings
    public float waveLength;
    //unit instantiate variables
    public GameObject unitDefault;
    private string team = "B"; 
    public Vector3 enemyBaseLocation = new Vector3(35,0,0);
    //currency
    public static int EnemyCurrency = 400;
    public void Miner()
    { 
        GameObject unitInstance;
        unitInstance = Instantiate(unitDefault, enemyBaseLocation, Quaternion.identity) as GameObject;
        unitInstance.GetComponent<UnitBrain>().Innit(team,UnitBrain.Classes.miner);
        unitInstance.GetComponent<UnitBrain>().onOrder(new Vector2(40,Random.Range(-10f,10f))); 
    }
    public void Scout()
    {
        GameObject unitInstance;
        unitInstance = Instantiate(unitDefault, enemyBaseLocation, Quaternion.identity) as GameObject;
        unitInstance.GetComponent<UnitBrain>().Innit(team,UnitBrain.Classes.scout);
        unitInstance.GetComponent<UnitBrain>().onOrder(new Vector2(-45f,0f)); 
    }
    public void Archer()
    {
        GameObject unitInstance;
        unitInstance = Instantiate(unitDefault, enemyBaseLocation, Quaternion.identity) as GameObject;
        unitInstance.GetComponent<UnitBrain>().Innit(team,UnitBrain.Classes.archer);
        unitInstance.GetComponent<UnitBrain>().onOrder(new Vector2(-45f,0f)); 
    }
    public void Knight()
    {
        GameObject unitInstance;
        unitInstance = Instantiate(unitDefault, enemyBaseLocation, Quaternion.identity) as GameObject;
        unitInstance.GetComponent<UnitBrain>().Innit(team,UnitBrain.Classes.knight);
        unitInstance.GetComponent<UnitBrain>().onOrder(new Vector2(-45f,0f)); 
    }
    public void Tank()
    {
        GameObject unitInstance;
        unitInstance = Instantiate(unitDefault, enemyBaseLocation, Quaternion.identity) as GameObject;
        unitInstance.GetComponent<UnitBrain>().Innit(team,UnitBrain.Classes.tank);
        unitInstance.GetComponent<UnitBrain>().onOrder(new Vector2(-45f,0f)); 
    }
    public void Giant()
    {
        GameObject unitInstance;
        unitInstance = Instantiate(unitDefault, enemyBaseLocation, Quaternion.identity) as GameObject;
        unitInstance.GetComponent<UnitBrain>().Innit(team,UnitBrain.Classes.tank);
        unitInstance.GetComponent<UnitBrain>().onOrder(new Vector2(-45f,0f)); 
    }
    
    public void SendWave()
    {
        foreach (GameObject Unit in GameObject.FindGameObjectsWithTag("Unit"))
        {
            if (Unit.GetComponent<UnitBrain>().teamCode == "B")
            {
                EnemyUnits.Add(Unit);
            }
        }
        Miner();
        waveLength = 10;
        StartCoroutine("WaveCounter",waveLength);
    }

    private IEnumerator WaveCounter(int waveLength)
    {
        while(waveLength>0)
        {
            waveLength -= 1;
            yield return new WaitForSeconds(1);
        }
        Debug.Log("Wave sent");
        SendWave();
    }
    // Start is called before the first frame update
    void Start()
    {
        Miner();
        StartCoroutine("WaveCounter",waveLength);
    }

    // Update is called once per frame
    void Update()
    {

    }
}
