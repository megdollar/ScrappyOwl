using UnityEngine;
using UnityEngine.UI;

//Controller class 
public class ScrappyOwlController : MonoBehaviour
{
    // Link to model, view, easy mode, hard mode and set initial score to 0
    public ScrappyOwlModel owlModel;
    public ScrappyOwlView owlView;
    public Button easy;
    public Button hard;
    public Button pause;
    public Button play;
   
    public Button settings;
    
    public Button showModeSelection;

    public Button showInstructionsBtn;
    public Slider musicSlider; // New Slider for music volume
    public InputField userName;
   
    public int score = 0;
    public bool pauseGame = false;
    public bool gameOver = false;
    public bool hardMode = false;
    public float musicVolume = 1.0f; // Initial music volume

    void Start()
    {

        // Show the home screen initially
        owlView.ShowHomeScreen();

        // Add listeners for play, pause, show score, difficulty buttons
        play.onClick.AddListener(PlayGame);
        pause.onClick.AddListener(PauseGame);
        
        easy.onClick.AddListener(StartEasyMode);
        hard.onClick.AddListener(StartHardMode);
        settings.onClick.AddListener(ShowSettingsScreen);
        
        showModeSelection.onClick.AddListener(ShowModeSelection);
        showInstructionsBtn.onClick.AddListener(showInstructions);


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
                owlModel.Jump();
            }

            //// Logs move accross the screen
            //owlView.LogMove();

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

        if (collision.gameObject.CompareTag("Ground"))
        {
            Debug.Log("Collision with ground");
            owlModel.isAlive = false;
            Time.timeScale = 0;
            ShowGameOver();
        }
        else if (collision.gameObject.CompareTag("Log"))
        {
            Debug.Log("Collision with log");
            owlModel.isAlive = false;
            DecreaseScore();
            //ShowGameOver();  
        }
    }

    // Method when play button is clicked
    public void PlayGame()
    {
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
        Application.Quit();
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
        owlView.UpdateDifficultyDisplay(false);
        // Debugging, remove later
        Debug.Log("Easy Mode");
        PlayGame();
    }

    // Method for Hard Mode
    public void StartHardMode()
    {
        hardMode = true;
        // Hard mode = true
        owlModel.SetDifficulty(true);
        owlView.UpdateDifficultyDisplay(true);
        // Debugging, remove late 
        Debug.Log("Hard Mode Selected");
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
            PauseGame();

        owlView.ShowSettingsScreen();
    }


    public void OnMusicSliderChanged(float value)
    {
        musicVolume = value;
    }


}
