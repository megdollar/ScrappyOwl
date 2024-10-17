using System.Collections;
using UnityEngine;

public class LogMoveScript : MonoBehaviour
{
    // Speed at which the log moves to the left
    public float moveSpeed = 5f; 

    // Position beyond which the log is deleted
    public float deadZone = -45f; 
    // Delay before the log is destroyed
    public float deleteDelay = 2f; 

    private bool isDeleting = false; 

    void Update()
    {
        // Move the log to the left
        transform.position += Vector3.left * moveSpeed * Time.deltaTime;

        // Check if the log has passed the dead zone and hasn't been flagged for deletion yet
        if (transform.position.x < deadZone && !isDeleting)
        {
            isDeleting = true; 
            StartCoroutine(DeleteLogAfterDelay());
        }
    }

    private IEnumerator DeleteLogAfterDelay()
    {
        // Wait for the specified delay time
        yield return new WaitForSeconds(deleteDelay);

        // Destroy the log after the delay
        Destroy(gameObject);
    }
}
