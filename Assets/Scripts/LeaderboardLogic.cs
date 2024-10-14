using System.Collections.Generic;
using UnityEngine;

public class LeaderboardLogic : MonoBehaviour
{
    public static LeaderboardLogic Instance;

    private List<HighScoreEntry> highScores = new List<HighScoreEntry>();

    private void Awake()
    {
        // Singleton pattern to ensure only one instance
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

    // Method to add a new high score
    public void AddHighScore(string initials, int score)
    {
        highScores.Add(new HighScoreEntry { initials = initials, score = score });
    }

    // Method to retrieve all high scores
    public List<HighScoreEntry> GetHighScores()
    {
        return highScores;
    }
}

// Class to represent a high score entry
[System.Serializable]
public class HighScoreEntry
{
    public string initials;
    public int score;
}
