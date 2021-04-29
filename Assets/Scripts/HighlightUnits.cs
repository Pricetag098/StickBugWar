using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HighlightUnits : MonoBehaviour
{
    public LayerMask whatToHighlight;
    public string teamCode = "A";
    public Vector2 clickPos;
    public Vector2 currentPos;

    GameObject square;

    bool record;
    List<UnitBrain> unitBrains = new List<UnitBrain>();
    // Start is called before the first frame update
    void Start()
    {

        square = transform.GetChild(0).gameObject;
        square.SetActive(false);
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetMouseButtonDown(0) && record == false)
        {
            clickPos = Camera.main.ScreenToWorldPoint(Input.mousePosition);
            record = true;
            square.SetActive(true);
            
        }
        if (record)
        {
            unitBrains.Clear();
            currentPos = Camera.main.ScreenToWorldPoint(Input.mousePosition);
            square.transform.position = midpoint(currentPos, clickPos);
            square.transform.localScale = new Vector3(currentPos.x - clickPos.x,currentPos.y - clickPos.y,1);
            Collider2D[] results = Physics2D.OverlapBoxAll(midpoint(currentPos, clickPos),
                new Vector2(
                    Mathf.Abs(currentPos.x - clickPos.x),
                    Mathf.Abs(currentPos.y - clickPos.y)
                    ),
                0,
                whatToHighlight);

            Debug.Log(results.Length);
            if (results.Length > 0)
            {

                for (int x = 0; x < results.Length; x++)
                {
                    
                   UnitBrain unitBrain = results[x].GetComponent<UnitBrain>();
                    if (unitBrain.teamCode == teamCode)
                    {
                        unitBrains.Add(unitBrain);
                        
                    }
                }
            }
            
        }
        if (Input.GetMouseButtonUp(0))
        {
            record = false;
            square.SetActive(false);
        }
        if (Input.GetMouseButtonDown(1))
        {
            for(int x = 0; x < unitBrains.ToArray().Length ; x++)
            {
                //Destroy(unitBrains[x].gameObject);
                unitBrains[x].onOrder(Camera.main.ScreenToWorldPoint(Input.mousePosition));
            }
        }
    }

    Vector2 midpoint(Vector2 p1, Vector2 p2)
    {
        return new Vector2((p1.x + p2.x) / 2, (p1.y + p2.y) / 2);
    }


    private void OnDrawGizmos()
    {
        if(unitBrains.ToArray().Length > 0)
        {
            for (int x = 0; x < unitBrains.ToArray().Length; x++)
            {
                Gizmos.color = Color.blue;
                if(unitBrains[x] != null)
                {
                    Gizmos.DrawWireSphere(unitBrains[x].transform.position, .3f);
                }
               

            }
        }
        
    }
}
