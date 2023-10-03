using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class RetryButton : MonoBehaviour
{

    public string gameScene = "Test_Map";

    private void Start()
    {
        Cursor.lockState = CursorLockMode.Confined;
    }
    public void RetryButtonClick()
    {
        SceneManager.LoadScene(gameScene);
    }
}
