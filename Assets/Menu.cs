using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Menu : MonoBehaviour
{
    public void QuitFun()
    {
        Application.Quit();
        Debug.Log("Quit");
    }
    public void PlayFun()
    {
        if(SceneManager.GetSceneByBuildIndex(buildIndex: 0).isLoaded == false)
        {
            SceneManager.LoadScene(0, LoadSceneMode.Single);
            Debug.Log("Play");
        }
        else
        {
            SceneManager.UnloadSceneAsync("Menu");
            Debug.Log("Resume");
        }
    }



    void Update()
    {
        if (Input.GetKeyDown("escape"))
        {
            SceneManager.LoadScene("Menu", LoadSceneMode.Additive);
        }
    }
}
