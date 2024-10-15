using UnityEngine;

public class ScrappyOwlModel : MonoBehaviour
{

    // Variables for the game
    public Rigidbody2D owlRigidbody;
    // Jump force for easy mode
    public float easyJump = 10f;
    // Jump force for hard mode
    public float hardJump = 3f;
    public float easyGravity = .5f;
    public float hardGravity = 1.5f;


    // Tracks if owl is alive
    public bool isAlive = true;
    // Position of the owl

    public Vector2 startingPosition = new Vector2(0, 0); // Adjust this to your desired starting point


    // Easy by default
    private bool hardMode = true;

    private void Start()
    {
        owlRigidbody = GetComponent<Rigidbody2D>();  // Get the Rigidbody2D attached to the owl
        SetDifficulty(false);  // Start in easy mode by default
    }

    // Method to change difficulty
    public void SetDifficulty(bool hardMode)
    {
        // Adjust gravity based on difficulty mode
        owlRigidbody.gravityScale = hardMode ? hardGravity : easyGravity;
    }



    // Method to handle owl's jump
    public void Jump()
    {
        if (owlRigidbody != null && isAlive)
        {
            float jumpForce = hardMode ? hardJump : easyJump;
            owlRigidbody.velocity = Vector2.up * jumpForce;
        }
    }

    // Method to reset the game
    /*public void ResetOwl()
    {
        owlRigidbody.position = startingPosition; // Set to starting position
        owlRigidbody.velocity = Vector2.zero; // Reset velocity
        isAlive = true; // Reset alive state
        
    } */

    public void ResetOwl()
    {
        // Set both the transform and Rigidbody2D position to the starting position
        //transform.position = startingPosition;
        owlRigidbody.position = startingPosition;
        owlRigidbody.velocity = Vector2.zero; // Reset velocity to avoid any movement after resetting
        isAlive = true; // Set the owl to alive state
    }



    // Method to get current position
    public Vector2 GetPosition()
    {
        return owlRigidbody.position;
    }

}
