using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UnitMovement : MonoBehaviour
{

    public float speed, minDistance = 0;
    public Vector2 target; // changed in the brain to decide destination
    

    Rigidbody2D rb;
    // Start is called before the first frame update
    void Start()
    {
        target = transform.position;
        rb = GetComponent<Rigidbody2D>();
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        // handles unit movemnent
        
        if(Vector2.Distance(transform.position, target) > minDistance)
        {

            rb.velocity = -((Vector2)transform.position-target).normalized * speed;
        }
        else
        {
            rb.velocity = Vector2.zero;
        }
    }
}
