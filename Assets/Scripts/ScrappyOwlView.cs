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
    public Button quitButton;

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
        currentPanel.SetActive(true);
    }

    // Method to show home screen
    public void ShowHomeScreen()
    {
        ShowPanel(homeScreen);
        
        homeScreen.SetActive(true);
        pauseScreen.SetActive(false);
        gameOverScreen.SetActive(false);
        gameScreen.SetActive(false);
        settingScreen.SetActive(false);
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
        settingScreen.SetActive(false);
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

    public void QuitGame()
    {
        Application.Quit();
    }
   
}