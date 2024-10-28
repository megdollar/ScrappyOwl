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
    public TextMeshProUGUI finalScoreText;
    public Text difficultyText;

    public GameObject owlSprite;
    public AudioSource flapAudioSource;

    private GameObject currentPanel;
    private GameObject previousPanel;

    public AudioSource owlAudioSource;
    public Animator animator;
    public GameObject startGameText;

    public GameObject explosionPrefab; 
    private AudioSource explosionAudioSource;

    // Method to show Home Screen by default
    void Start()
    {
        if (owlAudioSource == null)
        {
            owlAudioSource = GetComponent<AudioSource>();
        }

        if (flapAudioSource == null)
        {
            flapAudioSource = GetComponent<AudioSource>();
        }

        // Play the music
        if (owlAudioSource != null)
        {
            owlAudioSource.loop = true;
            owlAudioSource.Play();
        }

        if (explosionPrefab != null)
        {
            explosionAudioSource = explosionPrefab.GetComponent<AudioSource>();
        }


        ShowHomeScreen();

        // Get the Animator component
        if (animator == null)
            
            animator = owlSprite.GetComponent<Animator>();
            if (animator == null)
            {
                Debug.LogError("Animator component not found on OwlSprite");
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
        if (flapAudioSource != null)
        {
            flapAudioSource.enabled = false;
        }
    }

    // Method to show the pause screen
    public void ShowPauseScreen()
    {
        ShowPanel(pauseScreen);
    }

    // Method to show game screen
    public void ShowGameScreen()
    {
        ShowPanel(gameScreen);
    }

        public void PlayExplosion(Vector3 position)
    {
        GameObject explosion = Instantiate(explosionPrefab, position, Quaternion.identity);
        AudioSource explosionAudio = explosion.GetComponent<AudioSource>();

         if (explosionAudio != null)
        {
            explosionAudio.Play();
        }

        Destroy(explosion, 1f); 
    }

    // Method to show game-over screen and score
    public void ShowGameOverScreen(int score)
    {
        finalScoreText.text = score.ToString();
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

    // Method to show the settings screen
    public void ShowSettingsScreen()
    {
        ShowPanel(settingScreen);
    }

    // Method to show the mode selection screen
    public void ShowModeSelectionScreen()
    {
        ShowPanel(modeSelectionScreen);
    }

    // Method to show the instructions screen
    public void ShowInstructionsScreen()
    {
        ShowPanel(instructionsScreen);
    }

    // Method for the Back Button to return users to the previous panel
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

    // Hide all screens (used for resuming game or starting new one)
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

    // Method to update music volume
    public void UpdateMusicVolume(float volume)
    {
        if (owlAudioSource != null)
        {
            owlAudioSource.volume = volume;
        }
    }

    // Method to update flap sound volume
    public void UpdateFlapSoundVolume(float volume)
    {
        if (flapAudioSource != null)
        {
            flapAudioSource.volume = volume;
        }
        else
        {
            Debug.LogWarning("Flap AudioSource is not assigned.");
        }
    }

    public void FlapWings()
    {
        animator.SetBool("Flap", true); // Trigger the wing flap animation
        Invoke("ResetFlap", 0.2f); // Reset the animation after 0.2 seconds
    }

    private void ResetFlap()
    {
        animator.SetBool("Flap", false);
    }
}
