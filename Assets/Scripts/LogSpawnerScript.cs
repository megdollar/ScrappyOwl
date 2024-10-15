using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LogSpawnerScript : MonoBehaviour
{
    public GameObject log;
    public float spawnRate = 0.5f;
    private float timer = 0f;
    public float heightOffset = 1f;

    // Define a fixed lower range and an increased upper range for log heights
    public float minLogHeight = -3f;  // Minimum height offset from the center
    public float maxLogHeight = 6f;    // Maximum height offset from the center

    // Add this line to declare the logs list
    private List<GameObject> logs = new List<GameObject>(); // List to keep track of logs

    void Start()
    {
        spawnLog();  // Spawn an initial log at the start
    }

    void Update()
    {
        if (timer < spawnRate)
        {
            timer += Time.deltaTime;  // Increment timer
        }
        else
        {
            spawnLog();  // Spawn a new log
            timer = 0f;  // Reset the timer
        }
    }

    void spawnLog()
    {
        Camera cam = Camera.main;

        // Calculate the vertical center point of the camera's view
        float centerY = cam.transform.position.y;

        // Set a random Y position, allowing more height but keeping the lower limit
        float spawnYPosition = Random.Range(centerY + minLogHeight, centerY + maxLogHeight);

        // Debug log to see the spawn position
        Debug.Log($"Spawn Y Position: {spawnYPosition}");

        Vector3 spawnPosition = new Vector3(transform.position.x + cam.orthographicSize * cam.aspect, spawnYPosition, 0);

        GameObject newLog = Instantiate(log, spawnPosition, transform.rotation);
        newLog.tag = "Log";
        logs.Add(newLog); // Add the new log to the list

        BoxCollider2D collisionCollider = newLog.AddComponent<BoxCollider2D>();
        collisionCollider.isTrigger = false;  // Make sure it's a physical collider
    }

    // Make sure to include the HideLogs method you previously wrote
    public void HideLogs()
    {
        foreach (GameObject log in logs)
        {
            if (log != null) // Check if the log still exists
            {
                Destroy(log); // Destroy the log GameObject
            }
        }
        logs.Clear(); // Clear the list after destroying the logs
    }
}
