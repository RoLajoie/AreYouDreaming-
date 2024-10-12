using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MainMenu : MonoBehaviour
{
    private void Update()
    {
        Cursor.lockState = CursorLockMode.None;
    }

    public void startGame()
    {
        SceneManager.LoadScene(1);
    }

    public void restartGame()
    {
        SceneManager.LoadScene(0);
        FindObjectOfType<GameManager>().resetStats();
    }
    public void exitGame()
    {
        Application.Quit();
    }
}
