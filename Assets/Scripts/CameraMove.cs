using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraMove : MonoBehaviour
{
    public float moveSpeed = 5;
    private float direction;
    public Vector2 minMax = new Vector2(-13.6f,13.6f);

    // Update is called once per frame
    void Update()
    {
        direction = Input.GetAxis("Horizontal");
        transform.position += new Vector3(direction,0,0) * Time.deltaTime * moveSpeed;
        transform.position = new Vector3(Mathf.Clamp(transform.position.x, minMax.x, minMax.y),transform.position.y,transform.position.z);
    }
}
