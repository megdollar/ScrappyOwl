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
    public GameObject gameScreen;
    public GameObject backButtonPrefab;
    public GameObject quitButton;

    public GameObject instructionsScreen;
    public GameObject modeSelectionScreen;
    public Text leaderboardText;

    public TextMeshProUGUI scoreText;
    public Text difficultyText;  
    public Text highScoreText;

    public GameObject owlSprite;
    public GameObject[] logs; 

    // Variables for storing current and previous panels
    private GameObject currentPanel;
    private GameObject previousPanel;

    private int currentScore = 0;
    private int highScore = 0;

    // Method to show Home Screen by default
    void Start()
    {
        ShowHomeScreen();
    }

    // Method to show a specific panel and track the previous panel
    private void ShowPanel(GameObjectPanelToShow)
    {
        if (currentPanel != null)
        {
            previousPanel = currentPanel;
            currentPanel.SetActive(false);
        }

        currentPanel = panelToShow;
        currentPanel.SetActive(true);
    }

    // Method to show home screen
    public void ShowHomeScreen()
    {
        ShowPanel(homeScreen);
        
        homeScreen.SetActive(true);
        pauseScreen.SetActive(false);
        gameOverScreen.SetActive(false);
        scoreScreen.SetActive(false);
        gameScreen.SetActive(false);
        settingScreen.SetActive(false);
        leaderboardScreen.SetActive(false);
        modeSelectionScreen.SetActive(false);
        instructionsScreen.SetActive(false);

        //Adding Listener for the quit button
        quitButton.onClick.AddListener(QuitGame);
    }

    // Method to show the pause screen
    public void ShowPauseScreen()
    {
        ShowPanel(pauseScreen);
    }

    public void ShowGameScreen()
    {
        // Hide all panels when the game starts
        ShowPanel(null); 
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
        gameScreen.SetActive(false);
        modeSelectionScreen.SetActive(false);
        instructionsScreen.SetActive(false);
    }

    // Method to show game over screen and score
    public void ShowGameOverScreen(int score)
    {
        gameOverScreen.SetActive(true);
        scoreText.text = "Game Over! Your score: " + score.ToString();
        ShowPanel(gameOverScreen);

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
        ShowPanel(scoreScreen);
    }

    // Update the owl's position
    public void UpdateOwlPosition(Vector2 position)
    {
        owlSprite.transform.position = new Vector3(position.x, position.y, 0);
    }

    // Method to show the score in the UI
    public void UpdateScore(int newScore)
    {
        scoreText.text = newScore.ToString();
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
       ShowPanel(settingScreen);
    }

    public void ShowModeSelectionScreen()
    {
       ShowPanel(modeSelectionScreen);
    }

   public void ShowInstructionsScreen()
    {
        ShowPanel(instuctionScreen);
    }
    

    public void ShowLeaderboardScreen()
    {
        ShowPanel(leaderboardScreen);
    }

    // Method for the Back Button to return users to previous panel
    public void BackToPreviousPanel()
    {
        if (previousPanel != null)
        {
            ShowPanel(previousPanel);
        }
    }

    // Hide all screens (used for resuming game or starting new one
    public void HideAllPanels()
    {
        homeScreen.SetActive(false);
        pauseScreen.SetActive(false);
        gameOverScreen.SetActive(false);
        scoreScreen.SetActive(false);
        settingScreen.SetActive(false);
        leaderboardScreen.SetActive(false);
        gameScreen.SetActive(false);
        modeSelectionScreen.SetActive(false);
        instructionsScreen.SetActive(false);
    }
  
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





