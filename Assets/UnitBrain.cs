using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UnitBrain : MonoBehaviour
{
    UnitMovement unitMovement;
    // Start is called before the first frame update
    void Start()
    {
        unitMovement = GetComponent<UnitMovement>();
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
