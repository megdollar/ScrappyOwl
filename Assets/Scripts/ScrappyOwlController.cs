using System.Collections;
using UnityEngine;
using TMPro;
using UnityEngine.UI;
using UnityEngine.EventSystems;
using UnityEngine.SceneManagement;

// Controller class 
public class ScrappyOwlController : MonoBehaviour
{
    public ScrappyOwlModel owlModel;
    public ScrappyOwlView owlView;
    public Button pauseButton;
    public Slider musicSlider;
    public Slider flapSoundSlider;
    public int score = 0;
    public TMP_Text scoreText;
    public TMP_Text finalScoreText;
    public TMP_Text startGameText;
    public bool pauseGame = false;
    public bool gameOver = false;
    private bool startGame = false;
    public bool hardMode = false;
    public float musicVolume = 1.0f;
    public float flapSoundVolume = 1.0f;

    public LogSpawnerScript logSpawner;
    public gameObject startPanel;

    // Starting position for the owl
    private Vector2 startingPosition = new Vector2(44f, 12f);
    private AudioSource musicSource;

    void Start()
    {
        owlView.HideAllPanels();
        // Show the home screen initially
        owlView.ShowHomeScreen();

        musicSource = GetComponent<AudioSource>();
        if (musicSource != null)
        {
            musicSource.volume = musicVolume;
        }

        // Initialize the game with the beginning position
        owlModel.ResetOwl(startingPosition);

        // Set slider values and add listeners
        musicSlider.value = musicVolume;
        musicSlider.onValueChanged.AddListener(OnMusicSliderChanged);

        flapSoundSlider.value = flapSoundVolume;
        flapSoundSlider.onValueChanged.AddListener(OnFlapSoundSliderChanged);
    }

    void Update()
    {
        // Update the game if it is not paused/game over
        if (!pauseGame && !gameOver && startGame && owlModel.isAlive)
        {
            // Handle input to make the owl jump
            if (Input.GetKeyDown(KeyCode.Space) || Input.GetMouseButtonDown(0))
            {
                if (!IsPauseButtonClicked())
                {
                    owlModel.Jump();
                    owlView.flapAudioSource.Play(); // Play flap sound
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
        else if (!startGame)
        {
            // Check for user input to start the game
            if (Input.GetKeyDown(KeyCode.Space))
            {
                startGame = true;
                owlView.HideStartPanel();
                owlView.ShowGameScreen();
            }
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
            logSpawner.DestroyLogs();
        }

        // Store the final score to display in the game over UI
        int finalScore = score;

        // Display the final score in the game over screen
        if (owlView != null)
        {
            owlView.ShowGameOverScreen(finalScore);
        }

        // Reset the score for the next play session
        score = 0;
        owlView.UpdateScore(score);
    }




    // Handle owl's collision with branches and logs
    void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Log") || collision.gameObject.CompareTag("Ground"))
        {

            owlView.PlayExplosion(transform.position);
            StartCoroutine(DelayedActions());
            Invoke("ShowGameOver", 1f);     
        }
    }

    private IEnumerator DelayedActions()
    {
        yield return new WaitForSeconds(1f); 
        owlView.HideAllPanels();
    }



    public void PlayGame()
    {
        Time.timeScale = 1f;
        score = 0;
        owlView.UpdateScore(score);

        if (pauseGame)
        {
            ResumeGame();
        }
        else
        {
            NewGame();
        }

        // Hide the start Panel
        owlView.HideStartPanel();

        if (owlView.flapAudioSource != null)
        {
            owlView.flapAudioSource.enabled = true;
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
            logSpawner.HideTrees();
        }
    }

    // Method to resume the game after it was paused
    public void ResumeGame()
    {
        pauseGame = false;
        Time.timeScale = 1f;
        owlView.HideAllPanels();
        owlView.ShowGameScreen();
        logSpawner.ShowTrees();
    }

    // Method to start a new game
    public void NewGame()
    {
        Time.timeScale = 1f;
        pauseGame = false;
        gameOver = false;
        score = 0;
        owlView.UpdateScore(score);
        owlModel.ResetOwl(startingPosition);

        owlView.UpdateOwlPosition(owlModel.GetPosition());
        owlView.HideAllPanels();
        owlView.ShowGameScreen();

        // Set game to start
        startGame = true;
    }

    // Method to reset the game state
    public void ResetGameState()
    {
        Time.timeScale = 1f;
        pauseGame = false;
        gameOver = false;
        score = 0;
        owlView.UpdateScore(score);
        owlModel.ResetOwl(startingPosition);

        owlView.UpdateOwlPosition(owlModel.GetPosition());
        logSpawner.DestroyLogs();
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

    public void Jump()
    {
        owlModel.Jump();
        owlView.FlapWings();
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

    // Method to handle volume change from the music slider
    public void OnMusicSliderChanged(float value)
    {
        musicVolume = value;
        owlView.UpdateMusicVolume(musicVolume);
        PlayerPrefs.SetFloat("MusicVolume", musicVolume);
    }

    // Method to handle volume change from the flap sound slider
    public void OnFlapSoundSliderChanged(float value)
    {
        flapSoundVolume = value;
        owlView.UpdateFlapSoundVolume(flapSoundVolume);
        PlayerPrefs.SetFloat("FlapSoundVolume", flapSoundVolume);
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
        ResetGameState();
        owlView.ShowHomeScreen();
    }
}
