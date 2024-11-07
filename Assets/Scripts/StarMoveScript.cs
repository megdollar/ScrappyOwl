using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StarMoveScript : MonoBehaviour
{
    // Speed at which the Star moves to the left
    public float moveSpeed = 5f;

    // Position beyond which the Star is deleted
    public float deadZone = -45f;

    public AudioClip collisionSound;
    private AudioSource audioSource;

    void Start()
    {
        audioSource = gameObject.AddComponent<AudioSource>();
        audioSource.clip = collisionSound;
    }

    void Update()
    {
        transform.position = transform.position + (Vector3.left * moveSpeed) * Time.deltaTime;

        if (transform.position.x < deadZone)
        {
            // Destroy the Star after the delay
            Destroy(gameObject);
        }
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Owl")) // or whichever tag your owl has
        {
            // Play collision sound
            if (audioSource != null && collisionSound != null)
            {
                audioSource.Play();
            }
        }
    }
}
