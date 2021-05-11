using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Health : MonoBehaviour
{
    public float health,maxHealth;

    public bool isOre;
    public currency tower;
    public Gradient hpGradient;
    public SpriteRenderer sr;


    private void Start()
    {
        sr.color = hpGradient.Evaluate(health / maxHealth);
    }
    public void onTakeDmg(float dmg, string tc)
    {
        health -= dmg;
        if(health <= 0) { onDeath(); }

        sr.color = hpGradient.Evaluate(health / maxHealth);
        if (isOre)
        {
            if(tc == "A") { tower.addMoney((int)dmg * 50); }
            else { GameObject.FindGameObjectWithTag("EnemyTower").GetComponent<EnemyUnitSpawn>().EnemyCurrency += (int)dmg * 50; }
            
        }
    }
    public void onDeath()
    {
        Destroy(gameObject);
    }

}
