using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    public static GameManager Instance;
    public int score = 0;
    public int currentScore = 0; 

    void Awake()
    {
        
        if (Instance == null)
        {
            Instance = this;
            DontDestroyOnLoad(gameObject); 
        }
        else
        {
            Destroy(gameObject); 
        }
    }

    public void IncrementScore()
    {
        
        currentScore+=50;
        Debug.Log("Score: " + score);

    }

    public void respawnLevel()
    {
        int currentSceneIndex = SceneManager.GetActiveScene().buildIndex;
        currentScore = 0;
        SceneManager.LoadScene(currentSceneIndex);
    }

    public void resetStats()
    {
        currentScore = 0;
        score = 0; 
    }

    public void LoadNextScene()
    {
        score += currentScore; 
        
        int currentSceneIndex = SceneManager.GetActiveScene().buildIndex;
        int nextSceneIndex = currentSceneIndex + 1;
        currentScore = 0;
        if (nextSceneIndex < SceneManager.sceneCountInBuildSettings)
        {
            SceneManager.LoadScene(nextSceneIndex);
        }
        else
        {
            Debug.Log("No more scenes to load. Game completed!");
        }
    }
}
