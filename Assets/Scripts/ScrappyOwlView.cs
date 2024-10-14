using UnityEngine;
using UnityEngine.UI;

public class ScrappyOwlView : MonoBehaviour
{
    public GameObject homeScreen;
    public GameObject pauseScreen;
    public GameObject gameOverScreen;
    //public GameObject scoreScreen;
    public GameObject settingScreen;
    //public GameObject leaderboardScreen;
    public GameObject gameScreen;

    public GameObject instructionsScreen;
    public GameObject modeSelectionScreen;
    //public Text leaderboardText;

    public Text scoreText;
    public Text difficultyText;  
    public Text highScoreText;

    public GameObject owlSprite;
    public GameObject[] logs;  

    private int currentScore = 0;
    private int highScore = 0;

void Start()
{
    ShowHomeScreen();
}

    // Method to show home screen
    public void ShowHomeScreen()
    {

        homeScreen.SetActive(true);
        pauseScreen.SetActive(false);
        gameOverScreen.SetActive(false);
       // scoreScreen.SetActive(false);
        gameScreen.SetActive(false);
        settingScreen.SetActive(false);
      //  leaderboardScreen.SetActive(false);
        modeSelectionScreen.SetActive(false);
        instructionsScreen.SetActive(false);
    }

    // Method to show the pause screen
    public void ShowPauseScreen()
    {
        pauseScreen.SetActive(true);
    }

    public void ShowGameScreen()
    {
        HideScreens();
        gameScreen.SetActive(true); 
    }

    // Hide all the screens when game is playing
    public void HideScreens()
    {
        homeScreen.SetActive(false);
        pauseScreen.SetActive(false);
        gameOverScreen.SetActive(false);
       // scoreScreen.SetActive(false);
        settingScreen.SetActive(false);
       // leaderboardScreen.SetActive(false);
        gameScreen.SetActive(false);
        modeSelectionScreen.SetActive(false);
        instructionsScreen.SetActive(false);
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
   // public void ShowScoreScreen(int score)
   // {
   //     Debug.Log("show score screens method called");
    //    scoreScreen.SetActive(true);
    //    scoreText.text = "Current score: " + currentScore.ToString();
   // }

    // Update the owl's position
    public void UpdateOwlPosition(Vector2 position)
    {
        owlSprite.transform.position = new Vector3(position.x, position.y, 0);
    }

    // Method to show the score in the UI
    public void UpdateScore(int newScore)
    {
        currentScore += newScore;
        scoreText.text = "Score: " + currentScore.ToString();
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

    public void ShowModeSelectionScreen()
    {

        HideScreens();
        modeSelectionScreen.SetActive(true);
    }

   public void ShowInstructionsScreen()
    {
        HideScreens();
        instructionsScreen.SetActive(true);
    }

   // public void ShowLeaderboardScreen()
   // {
   //     HideScreens();  
   //     leaderboardScreen.SetActive(true);  

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







    /* DELETE THE FOLLOWING...
     * There should be a clear seperation of concerns: 
     * ScrappyOwlView handles the UI and game state, while LogMoveScript is responsible for moving the logs. 
     * This structure aligns with the MVC pattern, making our codebase more manageable and easier to understand. 
     * Thanks!
     -Ginger */

    //// Move logs across the screen during game
    //public void MoveLogs()
    //{
    //    float moveSpeed = 5f; //this is the speed, adjust as needed
    //    float resetPosition = -10f; //starting position on X where the log resets to
    //    float startPosition = 10f; // where the log starts off screen to the right

    //    // Iterate through the array of logs
    //    foreach (GameObject log in logs)
    //    {
    //        log.transform.position += Vector3.left * moveSpeed * Time.deltaTime;
    //        if (log.transform.position.x <= resetPosition)
    //        {
    //            log.transform.position = new Vector3(startPosition, log.transform.position.y, log.transform.position.z);
    //        }

    //    }
    //}
//}