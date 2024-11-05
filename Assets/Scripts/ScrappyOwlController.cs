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
    public bool pauseGame = false;
    public bool gameOver = false;
    public bool hardMode = false;
    public float musicVolume = 1.0f;
    public float flapSoundVolume = 1.0f;

    public LogSpawnerScript logSpawner;

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
        if (owlView.slowStartScreen.activeSelf)
        {
            // Hide the instructions panel and start the game when clicked or spacebar is pressed
            if (Input.GetKeyDown(KeyCode.Space) || Input.GetMouseButtonDown(0))
            {
                owlView.HideSlowStartScreen();
                PlayGame(); // Start the game
            }
            return; // Exit the update loop if instructions are active
        }
        // Update the game if it is not paused/game over
        if (!pauseGame && !gameOver && owlModel.isAlive)
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

    public void ShowGameOver()
    {
        owlModel.isAlive = false;
        gameOver = true;
        Time.timeScale = 0;

        // Call DestroyLogs from LogSpawnerScript
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

    // Removed OnCollisionEnter2D from this script

    public IEnumerator DelayedActions()
    {
        yield return new WaitForSeconds(0.7f);
        owlView.HideAllPanels();
        ShowGameOver();
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
    }

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

    // Method to show mode selection screen
    public void ShowModeSelection()
    {
        owlView.ShowModeSelectionScreen();
    }

    // Method to show instructions screen
    public void showInstructions()
    {
        owlView.ShowInstructionsScreen();
    }

    // Method for Easy Mode
    public void StartEasyMode()
    {
        hardMode = false;
        owlModel.SetDifficulty(false);
        owlModel.ResetOwl(startingPosition);

        owlView.UpdateOwlPosition(owlModel.GetPosition());
        owlView.slowStartScreen.SetActive(true);
        ShowSlowStartScreen();
    }

    // Method for Hard Mode
    public void StartHardMode()
    {
        hardMode = true;
        owlModel.SetDifficulty(true);
        owlModel.ResetOwl(startingPosition);

        owlView.UpdateOwlPosition(owlModel.GetPosition());
        owlView.slowStartScreen.SetActive(true);
        ShowSlowStartScreen();
    }

    public void ShowSlowStartScreen()
    {
        // Freeze the game time
        Time.timeScale = 0f;
        owlView.ShowGameScreen();
        // owlView.ShowSlowStartScreen(); // Assuming this method shows the panel with instructions
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
    public void IncreaseScoreByAmount(int amount)
    {
        score += amount;
        owlView.UpdateScore(score); // Update the score in the view
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