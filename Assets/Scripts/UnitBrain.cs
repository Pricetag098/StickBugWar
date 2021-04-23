using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UnitBrain : MonoBehaviour
{

    // setting the class automaticaly sets the values;
    public enum Classes { knight,archer,scout,tank };
    public Classes unitClass;


    [Header("Unit Values")]
   
    public string teamCode;
    public float viewRange = 1, attackRange = 1, attackTime = 1, attackDamage = 1;
    public int value;
    public enum targetingTypes { closest, furthest, value }
    public targetingTypes targetingType;

    [Space(10)]
    [Header("Individual Values")]
    public LayerMask whatIsUnit;
    public enum states { idle, attack, move, retreat }
    public states state;
    float attackTimer = 0;

    UnitMovement unitMovement;
    Health health;
    Transform target;
    // Start is called before the first frame update
    void Start()
    {
        unitMovement = GetComponent<UnitMovement>();
        health = GetComponent<Health>();


        switch (unitClass)
        {
            case Classes.knight:
                {
                    viewRange = 3;
                    attackRange = 1;
                    attackTime = 1;
                    attackDamage = 1;
                    health.maxHealth = 10;
                    health.health = health.maxHealth;
                    break;
                }
            case Classes.archer:
                {
                    viewRange = 3;
                    attackRange = 1;
                    attackTime = 1;
                    attackDamage = 1;
                    health.maxHealth = 5;
                    health.health = health.maxHealth;
                    break;
                }
            default:
                {
                    viewRange = 3;
                    attackRange = 1;
                    attackTime = 1;
                    attackDamage = 1;
                    health.maxHealth = 10;
                    health.health = health.maxHealth;
                    break;
                }
        }
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
            case states.idle: //used when neither in combat or motion, will still detect and fight enemys if in proximity
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



            case states.attack: //used when an enemy enters combat range
                {
                    if (target != null)
                    {


                        unitMovement.target = transform.position;
                        bool contAttack = false;
                        Collider2D[] attackResults = Physics2D.OverlapCircleAll(transform.position, viewRange, whatIsUnit);
                        if (attackResults.Length > 0)
                        {
                            for (int x = 0; x < attackResults.Length; x++)
                            {
                                if (attackResults[x].gameObject == target.gameObject)
                                {
                                    contAttack = true;
                                }
                            }
                        }
                        if (!contAttack)
                        {
                            state = states.move;
                        }

                        else
                        {
                            attackTimer -= Time.deltaTime;
                            if (attackTimer < 0)
                            {
                                Health tHealth = target.GetComponent<Health>();
                                tHealth.onTakeDmg(attackDamage);
                                attackTimer = attackTime;
                                print("Attack");
                            }


                        }
                        
                    }
                    else { state = states.idle; }
                    break;
                }



            case states.retreat:
                {
                    break;
                }



            case states.move:
                {
                    target = ScanForEnemy();
                    
                    if (target != null)
                    {
                        unitMovement.target = target.position;
                        print(gameObject.name + "Distance to target:" + Vector2.Distance(target.position, transform.position));
                        Collider2D[] attackResults = Physics2D.OverlapCircleAll(transform.position, attackRange, whatIsUnit);
                        if(attackResults.Length > 0)
                        {
                            for(int x = 0; x < attackResults.Length; x++)
                            {
                                if (attackResults[x].gameObject == target.gameObject)
                                {
                                    print(target.name);
                                    state = states.attack;
                                }
                            }
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
                    if(Vector2.Distance(transform.position, unitMovement.target)< .1f )
                    {
                        state = states.idle;
                    }
                    

                    break;
                }
        }


        
        
        
    }

    public Transform ScanForEnemy() //scans for enemys to approach and attack
    {
        Collider2D[] results = Physics2D.OverlapCircleAll(transform.position, viewRange, whatIsUnit);
        
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
        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(transform.position, attackRange);


    }
}
