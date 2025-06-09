using UnityEngine;

public class ScoreManager : MonoBehaviour
{
    public static ScoreManager Instance;

    public int playerScore = 0;
    public int agentScore = 0;

    void Awake()
    {
        if (Instance == null) Instance = this;
        else Destroy(gameObject);
    }

    public void AddPlayerScore(int amount)
    {
        playerScore += amount;
    }

    public void AddAgentScore(int amount)
    {
        agentScore += amount;
    }
}
