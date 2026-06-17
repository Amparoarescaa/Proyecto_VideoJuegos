
using UnityEngine;
using TMPro;

public class ScoreManager : MonoBehaviour
{
    public Transform player;
    public TMP_Text scoreText;

    private float startX;
    private int score;

    void Start()
    {
        if (player != null)
        {
            startX = player.position.x;
        }

        UpdateScoreText();
    }

    void Update()
    {
        if (player == null)
        {
            return;
        }

        score = Mathf.FloorToInt(player.position.x - startX);
        UpdateScoreText();
    }

    void UpdateScoreText()
    {
        if (scoreText != null)
        {
            scoreText.text = "Puntos: " + score;
        }
    }
}