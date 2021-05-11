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
    public int EnemyCurrency = 400;
    private int selector;
    public void Miner()
    { 
        GameObject unitInstance;
        unitInstance = Instantiate(unitDefault, enemyBaseLocation, Quaternion.identity) as GameObject;
        unitInstance.GetComponent<UnitBrain>().Innit(team,UnitBrain.Classes.miner);
        unitInstance.GetComponent<UnitBrain>().onOrder(new Vector2(40,Random.Range(-10f,10f))); 
        EnemyCurrency -= 50;
    }
    public void Scout()
    {
        GameObject unitInstance;
        unitInstance = Instantiate(unitDefault, enemyBaseLocation, Quaternion.identity) as GameObject;
        unitInstance.GetComponent<UnitBrain>().Innit(team,UnitBrain.Classes.scout);
        unitInstance.GetComponent<UnitBrain>().onOrder(new Vector2(-45f,0f));
        EnemyCurrency -= 75; 
    }
    public void Archer()
    {
        GameObject unitInstance;
        unitInstance = Instantiate(unitDefault, enemyBaseLocation, Quaternion.identity) as GameObject;
        unitInstance.GetComponent<UnitBrain>().Innit(team,UnitBrain.Classes.archer);
        unitInstance.GetComponent<UnitBrain>().onOrder(new Vector2(-45f,0f)); 
        EnemyCurrency -= 150;
    }
    public void Knight()
    {
        GameObject unitInstance;
        unitInstance = Instantiate(unitDefault, enemyBaseLocation, Quaternion.identity) as GameObject;
        unitInstance.GetComponent<UnitBrain>().Innit(team,UnitBrain.Classes.knight);
        unitInstance.GetComponent<UnitBrain>().onOrder(new Vector2(-45f,0f)); 
        EnemyCurrency -= 150;
    }
    public void Tank()
    {
        GameObject unitInstance;
        unitInstance = Instantiate(unitDefault, enemyBaseLocation, Quaternion.identity) as GameObject;
        unitInstance.GetComponent<UnitBrain>().Innit(team,UnitBrain.Classes.tank);
        unitInstance.GetComponent<UnitBrain>().onOrder(new Vector2(-45f,0f)); 
        EnemyCurrency -= 300;
    }
    public void Giant()
    {
        GameObject unitInstance;
        unitInstance = Instantiate(unitDefault, enemyBaseLocation, Quaternion.identity) as GameObject;
        unitInstance.GetComponent<UnitBrain>().Innit(team,UnitBrain.Classes.tank);
        unitInstance.GetComponent<UnitBrain>().onOrder(new Vector2(-45f,0f)); 
        EnemyCurrency -= 700;
    }
    
    public void SendWave()
    {
        Debug.Log(EnemyCurrency);
        Dictionary<UnitBrain.Classes, int> EnemyUnits = new Dictionary<UnitBrain.Classes, int>()
        {
            {UnitBrain.Classes.miner, 0},
            {UnitBrain.Classes.scout, 0},
            {UnitBrain.Classes.knight, 0},
            {UnitBrain.Classes.archer, 0},
            {UnitBrain.Classes.tank, 0},
            {UnitBrain.Classes.giant, 0}
        };

        foreach (GameObject Unit in GameObject.FindGameObjectsWithTag("Unit")) {

            if (Unit.GetComponent<UnitBrain>().teamCode == "B")
            {
                if (EnemyUnits.ContainsKey(Unit.GetComponent<UnitBrain>().unitClass))
                {
                    EnemyUnits[Unit.GetComponent<UnitBrain>().unitClass] += 1;
                }
            }
            Debug.Log(EnemyUnits);
        }
        if (EnemyUnits[UnitBrain.Classes.miner] < 3)
        {
            Miner();
        }
        if (EnemyCurrency >= 700)
        {
            Giant();
        }
        if (EnemyCurrency >= 300)
        {
            selector = Random.Range(0,3);
            Debug.Log("Selector:" + selector);
            if (selector == 0)
            {
                Archer();
                Archer();
            }
            if (selector == 1)
            {
                Archer();
                Knight();
            }
            if (selector == 2)
            {
                Knight();
                Knight();
            }
            if (selector == 3)
            {
                Tank();
            }
        }
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
