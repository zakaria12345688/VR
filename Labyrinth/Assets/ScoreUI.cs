using UnityEngine;
using TMPro;

public class ScoreUI : MonoBehaviour
{
    public TextMeshProUGUI agentScoreText;
    public TextMeshProUGUI playerScoreText;

    void Update()
    {
        if (ScoreManager.Instance != null)
        {
            agentScoreText.text = "Agent Score: " + ScoreManager.Instance.agentScore;
            playerScoreText.text = "Player Score: " + ScoreManager.Instance.playerScore;
        }
    }
}

