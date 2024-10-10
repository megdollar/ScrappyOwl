using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class ScrappyOwlView : MonoBehaviour
{
    public GameObject homeScreen;
    public GameObject pauseScreen;
    public GameObject gameOverScreen;
    public GameObject scoreScreen;
    public GameObject settingScreen;
    public GameObject leaderboardScreen;
    public Text leaderboardText;

    public TextMeshProUGUI scoreText;
    public Text difficultyText;  
    public Text highScoreText;

    public GameObject owlSprite;
    public GameObject[] logs;  

    private int currentScore = 0;
    private int highScore = 0;

    // Method to show home screen
    public void ShowHomeScreen()
    {
        homeScreen.SetActive(true);
        pauseScreen.SetActive(false);
        gameOverScreen.SetActive(false);
        scoreScreen.SetActive(false);
    }

    // Method to show the pause screen
    public void ShowPauseScreen()
    {
        pauseScreen.SetActive(true);
    }

    // Hide all the screens when game is playing
    public void HideScreens()
    {
        homeScreen.SetActive(false);
        pauseScreen.SetActive(false);
        gameOverScreen.SetActive(false);
        scoreScreen.SetActive(false);
        settingScreen.SetActive(false);
        leaderboardScreen.SetActive(false);
    }

    // Method to show game over screen and score
    public void ShowGameOverScreen(int score)
    {
        gameOverScreen.SetActive(true);
        scoreText.text = "Game Over! Your score: " + score.ToString();

        if (score > highScore)
        {
            highScore = score;
            PlayerPrefs.SetInt("HighScore", highScore);
            UpdateHighScoreText(highScore);
        }
        
    }


    // Show the score screen and display the current score
    public void ShowScoreScreen(int score)
    {
        scoreScreen.SetActive(true);
        scoreText.text = "Current score: " + currentScore.ToString();
    }

    // Update the owl's position
    public void UpdateOwlPosition(Vector2 position)
    {
        owlSprite.transform.position = new Vector3(position.x, position.y, 0);
    }

    // Method to show the score in the UI
    public void UpdateScore(int newScore)
    {
        currentScore += newScore;
        scoreText.text = currentScore.ToString();
    }



    public void UpdateHighScoreText(int highScore)
    {
        highScoreText.text = "High Score: " + highScore.ToString();
    }


    public void UpdateDifficultyDisplay(bool hardMode)
    {
        if (hardMode)
        {
            difficultyText.text = "Difficulty: Hard";
        }
        else
        {
            difficultyText.text = "Difficulty: Easy";
        }      
    }

    public void ShowSettingsScreen()
    {
        HideScreens();
        settingScreen.SetActive(true);
    }

    public void ShowLeaderboardScreen()
    {
        HideScreens();  
        leaderboardScreen.SetActive(true);  

        // Get the high scores from  LeaderboardLogic
        //List<HighScoreEntry> highScores = Leaderboard.Instance.GetHighScores();

        // String to hold the text
        // string leaderboardDisplay = "";

        // if (highScores.Count == 0)
        // {
        //     leaderboardDisplay += "You can be the top player, start a new game now!";
        // }
        // else
        // {
        //     foreach (HighScoreEntry entry in highScores)
        //     {
        //         leaderboardDisplay += entry.initials + "\t" + entry.score.ToString() + "\n";
        //     }
        // }

        // leaderboardText.text = leaderboardDisplay;
    }

}




