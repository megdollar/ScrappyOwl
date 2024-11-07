using UnityEngine;

public class ScrappyOwlModel : MonoBehaviour
{
    public Rigidbody2D owlRigidbody;
    public float easyJump = 10f;
    public float hardJump = 3f;
    public float easyGravity = 0.5f;
    public float hardGravity = 1.5f;
    public bool isAlive = true;
    private bool hardMode = true;

    public AudioClip collisionSound;

    private AudioSource audioSource;

    private ScrappyOwlController gameController;

    // Collision flag to prevent multiple collisions
    private bool hasCollided = false;
    public AudioSource collectibleAudioSource;
    public AudioClip acornCollectSound;
    public AudioClip starCollectSound;


    private void Start()
    {
        audioSource = GetComponent<AudioSource>();
        owlRigidbody = GetComponent<Rigidbody2D>();
        SetDifficulty(false);

        GameObject controllerObject = GameObject.Find("Controller");
        if (controllerObject != null)
        {
            gameController = controllerObject.GetComponent<ScrappyOwlController>();
        }

        audioSource = GetComponent<AudioSource>();
    }

    public void SetDifficulty(bool hardMode)
    {
        this.hardMode = hardMode; // Ensure hardMode is updated
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
        // Reset position and rotation
        transform.position = newStartingPosition;
        transform.rotation = Quaternion.identity;

        // Reset Rigidbody2D properties
        if (owlRigidbody != null)
        {
            owlRigidbody.velocity = Vector2.zero;
            owlRigidbody.angularVelocity = 0f;
            owlRigidbody.rotation = 0f;
            owlRigidbody.simulated = true;
            owlRigidbody.isKinematic = false;
            owlRigidbody.constraints = RigidbodyConstraints2D.None;
            owlRigidbody.Sleep();
            owlRigidbody.WakeUp();
        }

        // Re-enable collider
        Collider2D owlCollider = GetComponent<Collider2D>();
        if (owlCollider != null)
        {
            owlCollider.enabled = true;
        }

        // Reset isAlive flag and collision flag
        isAlive = true;
        hasCollided = false;
    }

    public Vector2 GetPosition()
    {
        return owlRigidbody.position;
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Acorn"))
        {
            gameController.IncreaseScoreByAmount(5);
            collectibleAudioSource.clip = acornCollectSound; // Set the acorn sound
            collectibleAudioSource.Play();
            Destroy(other.gameObject);
        }
        else if (other.CompareTag("Star"))
        {
            gameController.IncreaseScoreByAmount(10);
            collectibleAudioSource.clip = starCollectSound; // Set the star sound
            collectibleAudioSource.Play();
            Destroy(other.gameObject);
        }
        else if (other.CompareTag("LogTrigger"))
        {
            gameController.IncreaseScoreByAmount(1);
        }
    }


    // Moved OnCollisionEnter2D to this script
    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (!hasCollided && (collision.gameObject.CompareTag("Log") || collision.gameObject.CompareTag("Ground")))
        {
            Debug.Log("Collision detected with: " + collision.gameObject.name);
            hasCollided = true;

            isAlive = false; // Update the owl's state

            // Play the collision sound
            if (audioSource != null && collisionSound != null)
            {
                audioSource.PlayOneShot(collisionSound); // Play sound effect
            }

            // Play explosion and handle game over through the controller
            if (gameController != null)
            {
                gameController.owlView.PlayExplosion(transform.position);
                gameController.StartCoroutine(gameController.DelayedActions());
            }
        }
    }
}