using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LogSpawnerScript : MonoBehaviour
{
    public GameObject log;
    public float spawnRate = 3;
    private float timer = 0;
    public float heightOffset = 8;

    // Start is called before the first frame update
    void Start()
    {
        spawnLog();
    }

    // Update is called once per frame
    void Update()
    {
        if (timer < spawnRate)
        {
            timer = timer + Time.deltaTime;
            Debug.Log("Timer: " + timer); // Check timer value
        }
        else
        {
            spawnLog();
            timer = 0;
            Debug.Log("Log Spawned"); // Confirm log spawning

        }
    }

    // create function for spawning log that can be called elsewhere in code
    void spawnLog()
    {
        float lowestPoint = transform.position.y - heightOffset;
        float highestPoint = transform.position.y + heightOffset;

        //create an object at a specified position and rotation

        GameObject newLog = Instantiate(log, new Vector3(transform.position.x, Random.Range(lowestPoint, highestPoint), 0), transform.rotation);

        // Set the log's tag to "Log"
        newLog.tag = "Log";

        // Add a collider for collisions (e.g., the owl hitting the log)
        BoxCollider2D collisionCollider = newLog.AddComponent<BoxCollider2D>();
        collisionCollider.isTrigger = false; // This is the collision collider

        // Add a second collider for the trigger (e.g., detecting when the owl passes the log)
        //BoxCollider2D triggerCollider = newLog.AddComponent<BoxCollider2D>();
       // triggerCollider.isTrigger = true; // This is the trigger collider for passing by

        // Optionally set the tag for the trigger (if needed)
        //newLog.tag = "LogTrigger";

    }


}
