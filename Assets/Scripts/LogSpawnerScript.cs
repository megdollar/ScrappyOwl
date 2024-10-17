using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LogSpawnerScript : MonoBehaviour
{
    public GameObject log;
    public float spawnRate = 0.5f;
    private float timer = 0f;
    public float heightOffset = 1f;

    // Minimum height offset from the center
    public float minLogHeight = -3f;
    // Maximum height offset from the center
    public float maxLogHeight = 6f;

    // List to keep track of logs
    private List<GameObject> logs = new List<GameObject>();

    void Start()
    {
        // Spawn an initial log at the start
        spawnLog();
    }

    void Update()
    {
        if (timer < spawnRate)
        {
            // Increment timer
            timer += Time.deltaTime;
        }
        else
        {
            // Spawn a new log
            spawnLog();
            // Reset the timer
            timer = 0f;
        }
    }

    void spawnLog()
    {
        Camera cam = Camera.main;

        // Calculate the vertical center point of the camera's view
        float centerY = cam.transform.position.y;

        // Set a random Y position, allowing more height but keeping the lower limit
        float spawnYPosition = Random.Range(centerY + minLogHeight, centerY + maxLogHeight);

        Vector3 spawnPosition = new Vector3(transform.position.x + cam.orthographicSize * cam.aspect, spawnYPosition, 0);

        GameObject newLog = Instantiate(log, spawnPosition, transform.rotation);
        newLog.tag = "Log";
        // Add the new log to the list
        logs.Add(newLog);

        BoxCollider2D collisionCollider = newLog.AddComponent<BoxCollider2D>();
        // Make sure it's a physical collider
        collisionCollider.isTrigger = false;
    }

    // Method to destroy all logs
    public void DestroyLogs()
    {
        foreach (GameObject log in logs)
        {
            // Check if the log still exists
            if (log != null)
            {
                // Destroy the log GameObject
                Destroy(log);
            }
        }
        // Clear the list after destroying the logs
        logs.Clear();
    }

    // Method to hide all logs (make them invisible)
    public void HideTrees()
    {
        foreach (GameObject log in logs)
        {
            if (log != null)
            {
                log.SetActive(false); // Disable the entire GameObject
                Debug.Log("Hiding log: " + log.name);
            }
        }
    }

    // Method to show all logs (make them visible)
    public void ShowTrees()
    {
        foreach (GameObject log in logs)
        {
            if (log != null)
            {
                log.SetActive(true); // Enable the entire GameObject
                Debug.Log("Showing log: " + log.name);
            }
        }
    }
}
