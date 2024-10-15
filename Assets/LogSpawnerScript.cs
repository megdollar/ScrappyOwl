using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LogSpawnerScript : MonoBehaviour
{
    public GameObject log;  
    public float spawnRate = 3f;  
    private float timer = 0f;
    public float heightOffset = 8f;  

    // Start is called before the first frame update
    void Start()
    {
        spawnLog();  // Spawn an initial log at the start
    }

    // Update is called once per frame
    void Update()
    {
        // Timer to control spawning rate
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

    // Function to spawn a log at random heights within the camera's view
    void spawnLog()
    {
        // Get the camera's position
        Camera cam = Camera.main;

        // Get the vertical bounds of the camera's view
        float screenHeight = cam.orthographicSize;  // Half the height of the camera view
        float screenWidth = screenHeight * cam.aspect;  // Calculate the screen width based on aspect ratio

        // Define the vertical range for spawning logs, within the visible area
        float lowestPoint = cam.transform.position.y - screenHeight + heightOffset;
        float highestPoint = cam.transform.position.y + screenHeight - heightOffset;

        float spawnYPosition = Random.Range(lowestPoint, highestPoint);
        Vector3 spawnPosition = new Vector3(transform.position.x + screenWidth, spawnYPosition, 0);  

        GameObject newLog = Instantiate(log, spawnPosition, transform.rotation); 

        // Set the log's tag to "Log"
        newLog.tag = "Log";

        // Add a collision collider (BoxCollider2D) for physical collisions
        BoxCollider2D collisionCollider = newLog.AddComponent<BoxCollider2D>();
        collisionCollider.isTrigger = false;  // Make sure it's a physical collider

        // BoxCollider2D triggerCollider = newLog.AddComponent<BoxCollider2D>();
        // triggerCollider.isTrigger = true;  // This would be a trigger for passing by
    }
}
