using UnityEngine;

public class ScrappyOwlModel : MonoBehaviour
{
    public Rigidbody2D owlRigidbody;
    public float easyJump = 10f;
    public float hardJump = 3f;
    public float easyGravity = .5f;
    public float hardGravity = 1.5f;
    public bool isAlive = true;
    private bool hardMode = true;
    

    public AudioSource flapSound;

    private ScrappyOwlController gameController;

    private void Start()
    {
        owlRigidbody = GetComponent<Rigidbody2D>();
        SetDifficulty(false);

        gameController = FindObjectOfType<ScrappyOwlController>();
    }

    public void SetDifficulty(bool hardMode)
    {
        owlRigidbody.gravityScale = hardMode ? hardGravity : easyGravity;
    }

    public void Jump()
    {
        if (owlRigidbody != null && isAlive)
        {
            float jumpForce = hardMode ? hardJump : easyJump;
            owlRigidbody.velocity = Vector2.up * jumpForce;
        }
    }

    public void ResetOwl(Vector2 newStartingPosition)
    {
        owlRigidbody.position = newStartingPosition;
        owlRigidbody.rotation = 0f; 
        transform.position = newStartingPosition;
        transform.rotation = Quaternion.identity;  
        owlRigidbody.velocity = Vector2.zero;
        owlRigidbody.simulated = true;  // Ensure physics simulation is active

        
        Collider2D owlCollider = GetComponent<Collider2D>();
        if (owlCollider != null)
        {
            owlCollider.enabled = false;  
            owlCollider.enabled = true;  
        }
        isAlive = true;
    }


    public Vector2 GetPosition()
    {
        return owlRigidbody.position;
    }
}
