using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class HealthChecker : MonoBehaviour
{
    public Health TowerA;
    public Health TowerB;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (TowerA.health <= 0)
        {
            SceneManager.LoadScene("Menu", LoadSceneMode.Single);
        }
         if (TowerB.health <= 0)
        {
            SceneManager.LoadScene("Menu", LoadSceneMode.Single);
        }
    }
}
