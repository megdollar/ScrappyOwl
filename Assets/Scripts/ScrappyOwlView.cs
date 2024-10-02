using UnityEngine;

public class ScrappyOwlView : MonoBehaviour
{
    // Owl sprite is the game object 
    public GameObject owlSprite;  

    // Update the position of the owl
    public void UpdateOwlPosition(Vector2 position)
    {
        owlSprite.transform.position = new Vector3(position.x, position.y, 0);
    }

    // Game over view
    public void GameOver()
    {
        Debug.Log("Game Over!");
        // Check if the current score is new High Score
        if (score > highScore)
        {
            highScore = score;
            PlayerPrefs.SetInt("HighScore" , highScore);
            UpdateHighScoreText();
        }

    // Show the score
    public void UpdateScore(int newScore)
    {
        Debug.Log("Score: " + score);
        // TO DO, update the score on the screen
    }
}

}