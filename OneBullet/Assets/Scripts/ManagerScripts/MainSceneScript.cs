using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MainSceneScript : MonoBehaviour
{
    public string gameSceneName = "Test_Map";
   

    public void GoToGame()
    {
        SceneManager.LoadScene(gameSceneName);
    }
   
    public void ExitGame()
    {
        Application.Quit();
    }
}
