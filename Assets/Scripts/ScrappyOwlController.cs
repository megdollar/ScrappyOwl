using UnityEngine;
using TMPro;
using UnityEngine.UI;
using UnityEngine.EventSystems;
using UnityEngine.SceneManagement;

// Controller class 
public class ScrappyOwlController : MonoBehaviour
{
    // Link to model, view, easy mode, hard mode and set initial score to 0
    public ScrappyOwlModel owlModel;
    public ScrappyOwlView owlView;
    public Button pauseButton;
    public Slider musicSlider; 
    public int score = 0;
    public TMP_Text scoreText;
    public TMP_Text finalScoreText;
    public bool pauseGame = false;
    public bool gameOver = false;
    public bool hardMode = false;
    public float musicVolume = 1.0f;

    public LogSpawnerScript logSpawner;

    // Starting position for the owl
    private Vector2 startingPosition = new Vector2(44f, 12f);
    private AudioSource musicSource; 

    void Start()
    {
        owlView.HideAllPanels();
        // Show the home screen initially
        owlView.ShowHomeScreen();

        // Initialize the music volume from PlayerPrefs
        musicVolume = PlayerPrefs.GetFloat("MusicVolume", 1.0f);

        // Get the AudioSource component (ensure your GameObject has one)
        musicSource = GetComponent<AudioSource>();
        if (musicSource != null)
        {
            // Set the volume
            musicSource.volume = musicVolume; 
        }
        

        // Initialize the game with the beginning position
        owlModel.ResetOwl(startingPosition);

        // Set slider value and add listener
        musicSlider.value = musicVolume; 
        musicSlider.onValueChanged.AddListener(OnMusicSliderChanged); 
    }

    void Update()
    {
        // Update the game if it is not paused/game over
        if (!pauseGame && !gameOver && owlModel.isAlive)
        {
            // Handle input to make the owl jump
            if (Input.GetKeyDown(KeyCode.Space) || Input.GetMouseButtonDown(0))
            {
                if (!IsPauseButtonClicked())
                {
                    owlModel.Jump();
                }
            }

            // Check if the owl is still alive
            if (!owlModel.isAlive)
            {
                // If dead, game over
                ShowGameOver();
            }

            // Update the owl's position and view each frame
            owlView.UpdateOwlPosition(owlModel.GetPosition());
        }
    }

    private bool IsPauseButtonClicked()
    {
        if (EventSystem.current.IsPointerOverGameObject())
        {
            // Get the currently selected GameObject
            GameObject currentSelected = EventSystem.current.currentSelectedGameObject;

            // Check if the selected GameObject is the pause button
            if (currentSelected != null && currentSelected == pauseButton.gameObject)
            {
                return true;
            }
        }
        return false;
    }

    void OnTriggerEnter2D(Collider2D other)
    {
        // Check if the collided object has the tag "Log" or if it is on the correct layer
        if (other.CompareTag("LogTrigger"))  
        {
            // Increase score when the owl passes the log
            IncreaseScore();  
        }
    }

    public void ShowGameOver()
    {
        owlModel.isAlive = false;
        gameOver = true;
        Time.timeScale = 0;  

        // Call HideLogs from LogSpawnerScript
        if (logSpawner != null)
        {
            logSpawner.HideLogs();
        }


        if (owlView != null)
        {
            owlView.ShowGameOverScreen(score);
        }
        
    }

    // Handle owl's collision with branches and logs
    void OnCollisionEnter2D(Collision2D collision)
    {
        // Check if the collided object has the tag "Log"
        if (collision.gameObject.CompareTag("Log") || collision.gameObject.CompareTag("Ground"))
        {
            owlView.HideAllPanels();
            ShowGameOver();
        }
    }

    // Method when play button is clicked
    public void PlayGame()
    {
        Time.timeScale = 1f;
        if (pauseGame)
        {
            // The game was paused, keep playing
            ResumeGame();
        }
        else 
        {
            NewGame();
        }
    }

    // Method when pause button is clicked
    public void PauseGame()
    {
        if (!pauseGame && !gameOver)
        {
            pauseGame = true;
            Time.timeScale = 0f;
            owlView.ShowPauseScreen();
        }
    }

    // Method to resume the game after it was paused
    public void ResumeGame()
    {
        pauseGame = false;
        Time.timeScale = 1f;
        owlView.HideAllPanels();
        owlView.ShowGameScreen();
    }

    // Method to start a new game
    public void NewGame()
    {
        Time.timeScale = 1f;
        pauseGame = false;
        gameOver = false;
        score = 0;

        owlModel.ResetOwl(startingPosition);

        // Ensure view reflects the model's position
        owlView.UpdateOwlPosition(owlModel.GetPosition());
        owlView.HideAllPanels();
        owlView.ShowGameScreen();
    }


    // Method to modeSelection
    public void ShowModeSelection()
    {
        owlView.ShowModeSelectionScreen();
    }

    // Method to instructions
    public void showInstructions()
    {
        owlView.ShowInstructionsScreen();
    }

    // Method for Easy Mode
    public void StartEasyMode()
    {
        hardMode = false;
        // Easy mode = false
        owlModel.SetDifficulty(false);
        PlayGame();
    }

    // Method for Hard Mode
    public void StartHardMode()
    {
        hardMode = true;
        // Hard mode = true
        owlModel.SetDifficulty(true);
        PlayGame();
    }

    // Method to decrease score
    public void DecreaseScore()
    {
        if (score > 0)
        {
            score--;
        }
        owlView.UpdateScore(score);
    }

    // Method to increment the score
    [ContextMenu("Increase Score")]
    public void IncreaseScore()
    {
        score++;
        owlView.UpdateScore(score);
    }

    // Show Settings Screen method
    public void ShowSettingsScreen()
    {
        // Pause if game is playing
        if (!pauseGame)
        {
            PauseGame();
        }

        owlView.ShowSettingsScreen();
    }

    // Method to handle volume change from the slider
    public void OnMusicSliderChanged(float value)
    {
        musicVolume = value;
        owlView.UpdateMusicVolume(musicVolume); 
    }


    public void QuitGame()
    {
        
        // Check if the application is running in the editor
#if UNITY_EDITOR
        // If in the Unity editor, exit play mode
        UnityEditor.EditorApplication.ExitPlaymode();
#else
        // If in a built application, quit the application
        Application.Quit();
#endif
    }


    public void MainMenuButton()
    {
        owlView.ShowHomeScreen();
    }
}
