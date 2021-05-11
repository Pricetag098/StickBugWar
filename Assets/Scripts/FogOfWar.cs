using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FogOfWar : MonoBehaviour
{
    public Transform fog;
    public string tc;
    public float furtherstUnit;
    public float endpos;

    GameObject[] units;
    List<float> unitlocations = new List<float>();

    private void Update()
    {
        unitlocations.Clear();
        units = GameObject.FindGameObjectsWithTag("Unit");
        for(int x = 0; x < units.Length; x++)
        {
            if(units[x].GetComponent<UnitBrain>().teamCode == tc) { unitlocations.Add(units[x].transform.position.x + units[x].GetComponent<UnitBrain>().viewRange * 3); }
        }

        unitlocations.Sort();
        unitlocations.Reverse();
        furtherstUnit = unitlocations[0];
        fog.localScale = new Vector3(endpos - furtherstUnit, transform.localScale.y, 1);
        fog.transform.position = new Vector3((endpos + furtherstUnit) /2, 0,-1);


    }
}
