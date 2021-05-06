using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UnitBrain : MonoBehaviour
{

    // setting the class automaticaly sets the values;
    public enum Classes { knight, archer, scout, tank, giant, miner, tower, ore};
    public Classes unitClass;

    public bool testUnit = false;

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

    public Sprite[] icons;
    public SpriteRenderer sr;

    UnitMovement unitMovement;
    Health health;
    Transform target;
    // Start is called before the first frame update
    void Awake()
    {
        unitMovement = GetComponent<UnitMovement>();
        health = GetComponent<Health>();
        if (testUnit)
        {
            Innit(teamCode, unitClass);
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
                                onAttack();
                                //print("Attack");
                            }


                        }
                        
                    }
                    else { state = states.idle; }
                    break;
                }



            case states.retreat: //unused
                {
                    break;
                }



            case states.move: //used when the unit is in motion to a target
                {
                    target = ScanForEnemy();
                    
                    if (target != null)
                    {
                        unitMovement.target = target.position;
                        //print(gameObject.name + "Distance to target:" + Vector2.Distance(target.position, transform.position));
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
                if (results[x].GetComponent<UnitBrain>().teamCode != teamCode && results[x].GetComponent<UnitBrain>().teamCode != "R" && unitClass != Classes.miner)
                {
                    result.Add(results[x].GetComponent<UnitBrain>());
                }
                if (results[x].GetComponent<UnitBrain>().teamCode == "R" && unitClass == Classes.miner)
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

    

    public void onOrder(Vector2 order)
    {
        state = states.move;
        unitMovement.target = order;
    }



    /*CALL WHEN CREATING A UNIT
     * 
     * 1 instansiate unit
     * 2 do unit.UnitBrain.Innit( "the code for the team" , UnitBrain.Classes.'the class you want')
     * 3 idk hope it works lol
     * 
     */

    public void Innit(string tCode, Classes type)
    {
        unitClass = type;

        teamCode = tCode;
        
        switch (unitClass)
        {
            case Classes.knight:
                {
                    viewRange = 3;
                    attackRange = 1;
                    attackTime = 1;
                    attackDamage = 1;
                    health.maxHealth = 10;
                    unitMovement.speed = 3;
                    health.health = health.maxHealth;
                    sr.sprite = icons[1];
                    transform.localScale = Vector3.one * 1;
                    break;
                }
            case Classes.archer:
                {
                    viewRange = 3.5f;
                    attackRange = 2.1f;
                    attackTime = 3f;
                    attackDamage = 2;
                    health.maxHealth = 5;
                    unitMovement.speed = 2.75f;
                    health.health = health.maxHealth;
                    sr.sprite = icons[2];
                    transform.localScale = Vector3.one * 0.75f;
                    break;
                }
            case Classes.tank:
                {
                    viewRange = 3;
                    attackRange = 1;
                    attackTime = 3f;
                    attackDamage = .5f;
                    health.maxHealth = 30;
                    unitMovement.speed = 3;
                    health.health = health.maxHealth;
                    sr.sprite = icons[3];
                    transform.localScale = Vector3.one * 1.25f;
                    break;
                }
            case Classes.giant:
                {
                    viewRange = 2;
                    attackRange = 1.5f;
                    attackTime = 3f;
                    attackDamage = 5f;
                    health.maxHealth = 30;
                    unitMovement.speed = 1.5f;
                    health.health = health.maxHealth;
                    sr.sprite = icons[6];
                    transform.localScale = Vector3.one * 1.5f;
                    break;
                }
            case Classes.tower:
                {
                    viewRange = 0;
                    attackRange = 0;
                    attackTime = 0;
                    attackDamage = 0;
                    health.maxHealth = 1500;
                    unitMovement.speed = 0;
                    health.health = health.maxHealth;
                    sr.sprite = icons[7];
                    transform.localScale = Vector3.one * 5;
                    break;
                }
            case Classes.scout:
                {
                    viewRange = 2;
                    attackRange = 1;
                    attackTime = 1;
                    attackDamage = 0.5f;
                    health.maxHealth = 3;
                    unitMovement.speed = 6;
                    health.health = health.maxHealth;
                    sr.sprite = icons[5];
                    transform.localScale = Vector3.one * 5;
                    break;
                }
            case Classes.miner:
                {
                    viewRange = 3;
                    attackRange = 1;
                    attackTime = 1;
                    attackDamage = 1;
                    health.maxHealth = 10;
                    unitMovement.speed = 3;
                    health.health = health.maxHealth;
                    sr.sprite = icons[4];
                    transform.localScale = Vector3.one * 1;
                    break;
                }
            case Classes.ore:
                {
                    viewRange = 0;
                    attackRange = 0;
                    attackTime = 0;
                    attackDamage = 0;
                    health.maxHealth = 100;
                    unitMovement.speed = 0;
                    health.health = health.maxHealth;
                    sr.sprite = icons[0];
                    transform.localScale = Vector3.one * 1;
                    break;
                }
            default:
                {
                    viewRange = 0;
                    attackRange = 0;
                    attackTime = 0;
                    attackDamage = 0;
                    health.maxHealth = 100;
                    unitMovement.speed = 0;
                    health.health = health.maxHealth;
                    sr.sprite = icons[0];
                    transform.localScale = Vector3.one * 1;
                    break;
                }
        }
    }

    void onAttack()
    {

    }
    private void OnDrawGizmos()
    {
        Gizmos.color = Color.green;
        Gizmos.DrawWireSphere(transform.position, viewRange);
        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(transform.position, attackRange);


    }
}
