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

        BoxCollider2D collisionCollider = newLog.AddComponent<BoxCollider2D>();
        collisionCollider.isTrigger = false;  // Make sure it's a physical collider
    }
}
