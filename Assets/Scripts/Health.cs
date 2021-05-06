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
    public void onTakeDmg(float dmg)
    {
        health -= dmg;
        if(health <= 0) { onDeath(); }

        sr.color = hpGradient.Evaluate(health / maxHealth);
        if (isOre)
        {
            tower.addMoney((int)dmg * 50);
        }
    }
    public void onDeath()
    {
        Destroy(gameObject);
    }

}
