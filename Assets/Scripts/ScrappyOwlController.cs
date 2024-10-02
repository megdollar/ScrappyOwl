using UnityEngine;
using UnityEngine.UI;  

//Controller class 
public class ScrappyOwlController : MonoBehaviour
{
    // Lunk to model, view, easy mode, hard mode and set initial score to 0
    public ScrappyOwlModel owlModel;  
    public ScrappyOwlView owlView;    
    public Button easy;  
    public Button hard;  
    public int score = 0;

    void Start()
    {
        // Add listeners for the difficulty buttons
        easy.onClick.AddListener(StartEasyMode);
        hard.onClick.AddListener(StartHardMode);

        // Initialize the game with beginning position
        owlModel.ResetOwl();
        owlView.UpdateOwlPosition(owlModel.GetPosition());
    }

    void Update()
    {
        // Handle input to make the owl jump
        if (Input.GetKeyDown(KeyCode.Space) || Input.GetMouseButtonDown(0))
        {
            owlModel.Jump();
        }

        // Update the owl's position and view each frame
        owlModel.UpdatePosition(Time.deltaTime);
        owlView.UpdateOwlPosition(owlModel.GetPosition());

        // Check if the owl is still alive
        if (!owlModel.isAlive)
        {
            // If dead, game over
            owlView.GameOver();
        }
    }
    // TODO add method for hitting branch

    // Method for Easy Mode
    public void StartEasyMode()
    {
        // Easy mode = false
        owlModel.SetDifficulty(false);  
        // Debugging, remove later
        Debug.Log("Easy Mode");
    }

    // Method for Hard Mode
    public void StartHardMode()
    {
        // Hard mode = true
        owlModel.SetDifficulty(true);    
        // Debugging, remove late 
        Debug.Log("Hard Mode Selected");
    }

    // Method to increment the score
    public void IncreaseScore()
    {
        score++;
        owlView.UpdateScore(score);
    }
}
