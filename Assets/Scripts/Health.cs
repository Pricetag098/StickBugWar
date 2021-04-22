using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Health : MonoBehaviour
{
    public float health,maxHealth;


    public void onTakeDmg(float dmg)
    {
        health -= dmg;
    }
    public void onDeath()
    {
        Destroy(gameObject);
    }

}
