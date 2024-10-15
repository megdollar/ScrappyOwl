using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class ScrappyOwlView : MonoBehaviour
{
    public GameObject homeScreen;
    public GameObject pauseScreen;
    public GameObject gameOverScreen;
   
    public GameObject settingScreen;
 
    public GameObject gameScreen;
    public GameObject backButtonPrefab;
 

    public GameObject instructionsScreen;
    public GameObject modeSelectionScreen;

    public TextMeshProUGUI scoreText;
    public Text difficultyText;  

    public GameObject owlSprite;
    public GameObject[] logs; 

    // Variables for storing current and previous panels
    private GameObject currentPanel;
    private GameObject previousPanel;

    private int currentScore = 0;

    public AudioSource owlAudioSource;


    // Method to show Home Screen by default
    void Start()
    {
        ShowHomeScreen();
         if (owlAudioSource == null)
        {
            owlAudioSource = GetComponent<AudioSource>();
        }

    }

    // Method to show a specific panel and track the previous panel
    private void ShowPanel(GameObject panelToShow)
    {
        if (currentPanel != null)
        {
            previousPanel = currentPanel;
            currentPanel.SetActive(false);
        }

        currentPanel = panelToShow;
        if (currentPanel != null)
        {
            currentPanel.SetActive(true);
        }   
    }

    // Method to show home screen
    public void ShowHomeScreen()
    {
        ShowPanel(homeScreen);
        
    }

    // Method to show the pause screen
    public void ShowPauseScreen()
    {
        ShowPanel(pauseScreen);
    }

    public void ShowGameScreen()
    {
        ShowPanel(gameScreen); 
    }


    // Method to show game over screen and score
    public void ShowGameOverScreen(int score)
    {
        scoreText.text = "Game Over! Your score: " + score.ToString();
        ShowPanel(gameOverScreen);
        
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
        ShowPanel(instructionsScreen);
    }
    


    // Method for the Back Button to return users to previous panel
    public void BackToPreviousPanel()
    {
        if (previousPanel != null)
        {
            ShowPanel(previousPanel);
        }
        else
        {
            ShowHomeScreen();
        }
    }

    // Hide all screens (used for resuming game or starting new one
    public void HideAllPanels()
    {
        homeScreen.SetActive(false);
        pauseScreen.SetActive(false);
        gameOverScreen.SetActive(false);
        settingScreen.SetActive(false);
        gameScreen.SetActive(false);
        modeSelectionScreen.SetActive(false);
        instructionsScreen.SetActive(false);
    }


   
}