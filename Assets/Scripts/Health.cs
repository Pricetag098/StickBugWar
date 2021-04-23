using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Health : MonoBehaviour
{
    public float health,maxHealth;


    public void onTakeDmg(float dmg)
    {
        health -= dmg;
        if(health <= 0) { onDeath(); }
    }
    public void onDeath()
    {
        Destroy(gameObject);
    }

}
