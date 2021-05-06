using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FogOfWar : MonoBehaviour
{
    private new List<GameObject> PlayerUnits = new List<GameObject>();
    // Start is called before the first frame update
    void Start()
    {
        foreach (GameObject Unit in GameObject.FindGameObjectsWithTag("Unit"))
        {
            if (Unit.GetComponent<UnitBrain>().teamCode == "A")
            {
                PlayerUnits.Add(Unit);
            }
        }
        Debug.Log(PlayerUnits);
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
