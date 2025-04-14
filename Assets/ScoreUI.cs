using UnityEngine;
using UnityEngine.UI;

public class ScoreUI : MonoBehaviour
{
    public Text scoreText; // Refer�ncia ao texto da UI
    private int score;

    public void AddPoints(int points)
    {
        score += points;
        scoreText.text = $"Pontos: {score}";
    }
}
