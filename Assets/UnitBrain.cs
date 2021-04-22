using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UnitBrain : MonoBehaviour
{
    public string teamCode;
    public float viewRange = 1;
    public int value;
    UnitMovement unitMovement;

    public enum targetingTypes { closest, furthest, value}
    public enum state { retreat, attack, move}
    public targetingTypes targetingType;
    // Start is called before the first frame update
    void Start()
    {
        unitMovement = GetComponent<UnitMovement>();
    }

    // Update is called once per frame
    void Update()
    {
        //temp
        /*
        if (Input.GetMouseButtonDown(0))
        {
            Vector3 mouse = Input.mousePosition;
            unitMovement.target = Camera.main.ScreenToWorldPoint(mouse);
        }*/


        Transform target = ScanForEnemy();
        if (target != null)
        {
            unitMovement.target = (Vector2)target.position;
        }
        else
        {
            if (Input.GetMouseButtonDown(0) && teamCode =="A")
            {
                Vector3 mouse = Input.mousePosition;
                unitMovement.target = Camera.main.ScreenToWorldPoint(mouse);
            }
            else
            {
               // unitMovement.target = transform.position;
            }
            
        }
        
        
    }

    public Transform ScanForEnemy()
    {
        Collider2D[] results = Physics2D.OverlapCircleAll(transform.position, viewRange, LayerMask.NameToLayer("Unit"));
        
        List<UnitBrain> result = new List<UnitBrain>();
        if (results.Length > 0)
        {
            for (int x = 0; x < results.Length; x++)
            {
                if (results[x].GetComponent<UnitBrain>().teamCode != teamCode)
                {
                    result.Add(results[x].GetComponent<UnitBrain>());
                }
            }
        }
        

        if (result.ToArray().Length > 0)
        {
            int bestTarget = 0;
            for (int x = 0; x < result.ToArray().Length; x++)
            {
                
                switch (targetingType)
                {
                    case targetingTypes.closest:
                        {
                            if (Vector2.Distance(result[x].transform.position, transform.position) < Vector2.Distance(result[bestTarget].transform.position, transform.position))
                            {
                                bestTarget = x;
                            }
                            break;
                        }
                    case targetingTypes.furthest:
                        {
                            if (Vector2.Distance(result[x].transform.position, transform.position) > Vector2.Distance(result[bestTarget].transform.position, transform.position))
                            {
                                bestTarget = x;
                            }
                            break;
                        }
                    case targetingTypes.value:
                        {
                            break;
                        }

                }
            }
            return result[bestTarget].transform;
        }

        return null;

    }
    private void OnDrawGizmos()
    {
        Gizmos.color = Color.green;
        Gizmos.DrawWireSphere(transform.position, viewRange);
    }
}
