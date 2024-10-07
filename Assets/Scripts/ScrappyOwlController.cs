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
    public Button showScore;
    public Button settings;
    public Slider musicSlider; // New Slider for music volume
    public Button musicToggle; // Toggle music on/off
    public InputField userName;
    public Text scoreText;
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
        showScore.onClick.AddListener(ShowScore);
        easy.onClick.AddListener(StartEasyMode);
        hard.onClick.AddListener(StartHardMode);
        settings.onClick.AddListener(ShowSettingsScreen);
        if (musicToggle != null)
            musicToggle.onClick.AddListener(ToggleMusic);

        musicVolume = PlayerPrefs.GetFloat("MusicVolume" , 1.0f);

        // Initialize the game with beginning position
        owlModel.ResetOwl();
        owlView.UpdateOwlPosition(owlModel.GetPosition());
    }

    void Update()
    {
         // Update the game if it is not paused/game over
        if (!pauseGame && !gameOver)
        {
            // Handle input to make the owl jump
            if (Input.GetKeyDown(KeyCode.Space) || Input.GetMouseButtonDown(0))
            {
                owlModel.Jump();
            }

            // Update the owl's position and view each frame
            owlModel.UpdatePosition(Time.deltaTime);
            owlView.UpdateOwlPosition(owlModel.GetPosition());

            //// Logs move accross the screen
            //owlView.LogMove();

            // Check if the owl is still alive
            if (!owlModel.isAlive)
            {
                // If dead, game over
                owlView.ShowGameOverScreen(score);
            }
        }
    }

    // Handle owl's collision with branches
    void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Branch"))
        {
            owlModel.isAlive = false;  
            DecreaseScore();  
            ShowGameOver();  
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
        owlView.HideScreens();
    }

    // Method to start a new game
    public void NewGame()
    {
        pauseGame = false;
        gameOver = false;
        score = 0;
        owlModel.ResetOwl();  
        owlView.HideScreens();
    }

    // Method to show score when score button is pressed
    public void ShowScore()
    {
        owlView.ShowScoreScreen(score);
    }

    // Method to show game over screen and update the score
    public void ShowGameOver()
    {
        gameOver = true;
        owlView.ShowGameOverScreen(score);

        if (score > owlView.highScore)
        {
            owlView.highScore = score;
            PlayerPrefs.SetInt("HighScore" , owlViewhighScore);
            owlView.UpdateHighScoreText();
        }
    }



    // Method for Easy Mode
    public void StartEasyMode()
    {
        hardMode = false;
        // Easy mode = false
        owlModel.SetDifficulty(false);  
        owlView.UpdateDifficultyDisplay(false, score);
        // Debugging, remove later
        Debug.Log("Easy Mode");
    }

    // Method for Hard Mode
    public void StartHardMode()
    {
        hardMode = true;
        // Hard mode = true
        owlModel.SetDifficulty(true); 
        owlView.UpdateDifficultyDisplay(true, score);   
        // Debugging, remove late 
        Debug.Log("Hard Mode Selected");
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

    public void ToggleMusic()
    {
        musicVolume = musicVolume == 0.0f ? 1.0f : 0.0f;
    }

    public void OnMusicSliderChanged(float value)
    {
        musicVolume = value;
    }
}
