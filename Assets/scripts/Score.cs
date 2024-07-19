using UnityEngine;
using UnityEngine.UI;

public class ScoreManager : MonoBehaviour
{
    public Text scoreText; // Reference to the UI Text element for displaying the score
    private int score = 0; // Variable to keep track of the score

    void Start()
    {
        UpdateScoreText(); // Initialize the score text
    }

    // Method to increment the score and update the UI
    public void AddScore(int value)
    {
        score += value;
        Debug.Log("Score updated: " + score); // Debug log for score update
        UpdateScoreText();
    }

    // Method to update the score text
    private void UpdateScoreText()
    {
        if (scoreText != null)
        {
            scoreText.text = "Score: " + score;
        }
        else
        {
            Debug.LogError("Score Text is not assigned in ScoreManager.");
        }
    }
}
