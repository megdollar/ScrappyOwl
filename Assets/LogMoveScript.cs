using System.Collections;
using UnityEngine;

public class LogMoveScript : MonoBehaviour
{
    public float moveSpeed = 5;
    public float deadZone = -45;
    public float deleteDelay = 2f; // Delay before deletion in seconds

    void Update()
    {
        // Move the log to the left
        transform.position += Vector3.left * moveSpeed * Time.deltaTime;

        // Check if the log is past the deadZone
        if (transform.position.x < deadZone)
        {
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
