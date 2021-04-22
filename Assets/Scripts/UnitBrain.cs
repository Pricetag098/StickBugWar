using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UnitBrain : MonoBehaviour
{
    public string teamCode;
    public float viewRange = 1, attackRange = 1;

    public int value;
    UnitMovement unitMovement;

    public enum targetingTypes { closest, furthest, value}
    public enum states { idle, attack, move, retreat }
    public targetingTypes targetingType;
    public states state;


    Transform target;
    Transform enemyTarget;
    // Start is called before the first frame update
    void Start()
    {
        unitMovement = GetComponent<UnitMovement>();
    }

    // Update is called once per frame
    private void Update()
    {
        stateMachine();
    }
    void stateMachine()
    {
        switch (state)
        {
            case states.idle:
                {
                    if(ScanForEnemy() != null)
                    {
                        state = states.move;
                    }
                    else
                    {
                        unitMovement.target = transform.position;
                    }
                    break;
                }



            case states.attack:
                {
                    unitMovement.target = transform.position;
                    break;
                }



            case states.retreat:
                {
                    break;
                }



            case states.move:
                {
                    target = ScanForEnemy();
                    enemyTarget = target;
                    if (target != null)
                    {
                        unitMovement.target = (Vector2)target.position;
                        print(gameObject.name + "Distance to target:" + Vector2.Distance(target.position, transform.position));
                        if (Physics2D.Raycast(transform.position, -(transform.position - enemyTarget.position).normalized,attackRange))
                        {
                            state = states.attack;
                        }
                    }
                    else
                    {
                        if (Input.GetMouseButtonDown(0) && teamCode == "A")
                        {
                            Vector3 mouse = Input.mousePosition;
                            unitMovement.target = Camera.main.ScreenToWorldPoint(mouse);

                        }

                    }
                    

                    break;
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
        if(target != null)
        {
            Gizmos.color = Color.red;
            Gizmos.DrawLine(transform.position,transform.position+ -(transform.position - enemyTarget.position).normalized * attackRange);
        }
        
    }
}
