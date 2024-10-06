using UnityEngine;

public class ScrappyOwlModel : MonoBehaviour
{

    // Variables for the game
    // How fast owldrops down
    public float gravity;
    // How high the owl jumps
    public float jumpHeight;
    // Current velocity of owl
    public Vector2 velocity;

    // Tracks if owl is alive
    public bool isAlive = true;
    // Position of the owl
    private Vector2 position;

    // Change the mode to easy or hard setting the gravity and jumpHeight
    public void SetDifficulty(bool hardMode)
    {
        if (hardMode)
        {
            // Hard mode settings
            // 15 is stronger than the eath's pull
            gravity = -15f;
            // lower jump 
            jumpHeight = 4f;
        }
        else
        {
            // Easy mode settings
            // 9.8 m/s close to earth's pull
            gravity = -9.8f;
            // higher jump 
            jumpHeight = 7f;
        }
    }

    // Method to update the postition
    public void UpdatePosition(float deltaTime)
    {
        if (isAlive)
        {
            velocity.y += gravity * deltaTime;
            position += velocity * deltaTime;

        }
    }

    // Method to handle owl's jump
    public void Jump()
    {
        if (isAlive)
        {
            velocity.y = jumpHeight;
        }
    }

    // Method to reset the game
    public void ResetOwl()
    {
        position = Vector2.zero;
        velocity = Vector2.zero;
        isAlive = true;
    }

    // Method to get current position
    public Vector2 GetPosition()
    {
        return position;
    }

}
