using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UnitSpawner : MonoBehaviour
{
    public GameObject unitDefault;
    public Vector3 enemyBaseLocation;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if(Input.GetKeyDown("s"))
        {
            GameObject unitInstance;
            //unitInstance = Instantiate(unitDefault, enemyBaseLocation, Quaternion.identity) as GameObject;
            //unitInstance.GetComponent<UnitBrain.Classes.tank>();
        }
    }
}