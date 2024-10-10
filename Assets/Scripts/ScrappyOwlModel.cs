using UnityEngine;

public class ScrappyOwlModel : MonoBehaviour
{

    // Variables for the game
    public Rigidbody2D owlRigidbody;
    // Jump force for easy mode
    public float easyJump = 7f;
    // Jump force for hard mode
    public float hardJump = 4f;  
    public float easyGravity = 1f;  
    public float hardGravity = 1.5f;


    // Tracks if owl is alive
    public bool isAlive = true;
    // Position of the owl
    private Vector2 position;

    // Easy by default
    private bool hardMode = true;

    private void Start()
    {
         if (owlRigidbody == null)
        {
            owlRigidbody = GetComponent<Rigidbody2D>();
        }
        SetDifficulty(false);  // Start in easy mode by default
    }

    // Method to change difficulty
    public void SetDifficulty(bool hardMode)
    {
        this.hardMode = hardMode;
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
    public void ResetOwl()
    {
        owlRigidbody.position = Vector2.zero;
        owlRigidbody.velocity = Vector2.zero;
        isAlive = true;
    }

    // Method to get current position
    public Vector2 GetPosition()
    {
        return owlRigidbody.position;
    }

}
