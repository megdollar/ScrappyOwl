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
        // TO DO, show a game over screen
    }

    // Show the score
    public void UpdateScore(int score)
    {
        Debug.Log("Score: " + score);
        // TO DO, update the score on the screen
    }
}
