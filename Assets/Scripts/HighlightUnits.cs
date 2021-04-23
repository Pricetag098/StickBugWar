using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HighlightUnits : MonoBehaviour
{
    
    public Vector2 clickPos;
    public Vector2 currentPos;

    GameObject square;

    bool record;

    // Start is called before the first frame update
    void Start()
    {
        square = transform.GetChild(0).gameObject;
        square.SetActive(false);
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetMouseButtonDown(1) && record == false)
        {
            clickPos = Camera.main.ScreenToWorldPoint(Input.mousePosition);
            record = true;
            square.SetActive(true);
        }
        if (record)
        {
            currentPos = Camera.main.ScreenToWorldPoint(Input.mousePosition);
            square.transform.position = midpoint(currentPos, clickPos);
            square.transform.localScale = new Vector3(currentPos.x - clickPos.x,currentPos.y - clickPos.y,1);
            
        }
        if (Input.GetMouseButtonUp(1))
        {
            record = false;
            square.SetActive(false);
        }
    }

    Vector2 midpoint(Vector2 p1, Vector2 p2)
    {
        return new Vector2((p1.x + p2.x) / 2, (p1.y + p2.y) / 2);
    }
}
