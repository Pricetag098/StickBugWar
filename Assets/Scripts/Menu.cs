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
        if(SceneManager.GetSceneByBuildIndex(buildIndex: 1).isLoaded == false)
        {
            SceneManager.LoadScene(1, LoadSceneMode.Single);
            Debug.Log("Play");
        }
        else
        {
            SceneManager.UnloadSceneAsync("Menu");
            Time.timeScale = 1f;
            Debug.Log("Resume");
        }
    }

    public void Pause()
    {
        Debug.Log("Pause");
        SceneManager.LoadScene("Menu", LoadSceneMode.Additive);
        Time.timeScale = 0f;
    }


    void Update()
    {
        if (Input.GetKeyDown("escape"))
        {
            Pause();
        }
    }
}
