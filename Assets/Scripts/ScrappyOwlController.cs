using UnityEngine;
using TMPro;
using UnityEngine.UI;
using UnityEngine.EventSystems;


//Controller class 
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


    void Start()
    {
        owlView.HideAllPanels();
        // Show the home screen initially
        owlView.ShowHomeScreen();

        musicVolume = PlayerPrefs.GetFloat("MusicVolume", 1.0f);

        // Initialize the game with beginning position
        owlModel.ResetOwl();
        owlView.UpdateOwlPosition(owlModel.GetPosition());
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


    // Method to increase score when owl passes by logs
    void OnTriggerEnter2D(Collider2D other)
    {

        if (other.CompareTag("LogTrigger"))
            Debug.Log("Owl passed the log!");
        IncreaseScore();
    }
// Method to show game over screen and update the score
    public void ShowGameOver()
    {
        gameOver = true;
        //SaveScore();
        owlView.ShowGameOverScreen(score);
    }

    // Handle owl's collision with branches
    void OnCollisionEnter2D(Collision2D collision)
    {

        Debug.Log("Owl collided with: " + collision.gameObject.name);

        owlModel.isAlive = false;
        Time.timeScale = 0;
        ShowGameOver();
        
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
        else if (gameOver)
        {
            // Reset and start the game
            NewGame();
        }
        else
        {
            // Start game from home screen
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
    }

    // Method to quit the game
    public void QuitGame()
    {
        owlView.ShowHomeScreen();
    }

    // Method to start a new game
    public void NewGame()
    {
        pauseGame = false;
        gameOver = false;
        score = 0;
        owlModel.ResetOwl();
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
        scoreText.text = score.ToString();
        finalScoreText.text = score.ToString();
        owlView.UpdateScore(score);
    }

    // Show Settings Screen method
    public void ShowSettingsScreen()
    {
        // Pause if game is playing
        if (!pauseGame)
            PauseGame();

        owlView.ShowSettingsScreen();
    }


    public void OnMusicSliderChanged(float value)
    {
        musicVolume = value;
    }


}
