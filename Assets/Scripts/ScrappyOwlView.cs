using UnityEngine;
using UnityEngine.UI;

public class ScrappyOwlView : MonoBehaviour
{
    public GameObject homeScreen;
    public GameObject pauseScreen;
    public GameObject gameOverScreen;
    public GameObject scoreScreen;
    public Text scoreText;
    public Text difficultyText;  

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
    }

    // Method to show game over screen and score
    public void ShowGameOverScreen(int score)
    {
        gameOverScreen.SetActive(true);
        scoreText.text = "Game Over! Your score: " + score.ToString();

        if (currentScore > highScore)
        {
            highScore = currentScore;
            PlayerPrefs.SetInt("HighScore", highScore);
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
        scoreText.text = "Score: " + currentScore.ToString();
    }

    // Display the difficulty level 
    public void UpdateDifficultyDisplay(bool hardMode, int score){
    {

        Debug.Log("Game Over!");
        // Check if the current score is new High Score
        if (score > highScore)
        {
            highScore = score;
            PlayerPrefs.SetInt("HighScore" , highScore);
            // please add this method below or the logic that needs to be here
            //UpdateHighScoreText();
        }
    }


    public void UpdateHighScoreText();
    {
        highScoreText.text = "High Score: " + highScore.ToString();
    }
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
        settingsScreen.SetActive(true);
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
}




