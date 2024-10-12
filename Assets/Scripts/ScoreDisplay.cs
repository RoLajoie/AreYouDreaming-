using UnityEngine;
using UnityEngine.UI;

public class ScoreDisplay : MonoBehaviour
{
    public Text scoreText; 
    private GameManager gameManager;
    public bool final; 

    private void Start()
    {
        gameManager = GameManager.Instance;

        if (scoreText == null)
        {
            Debug.LogError("Score Text is not assigned in the Inspector!");
        }
    }

    private void Update()
    {
        

        if (final)
        {
            scoreText.text = "Final Score: " + gameManager.score;
        } else
        {
            scoreText.text = "Score: " + gameManager.currentScore;
        }

    }
}
