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
        }
        else
        {
            spawnLog();
            timer = 0;

        }
    }

    // create function for spawning log that can be called elsewhere in code
    void spawnLog()
    {
        float lowestPoint = transform.position.y - heightOffset;
        float highestPoint = transform.position.y + heightOffset;

        //create an object at a specified position and rotation
        GameObject newLog = Instantiate(log, new Vector3(transform.position.x, Random.Range(lowestPoint, highestPoint), 0), transform.rotation);
        
        // Ensure the log has a BoxCollider2D
        AddColliderToLog(newLog);

        newLog.tag = "Log";
    }

     // Method to ensure a BoxCollider2D is added to each log
    void AddColliderToLog(GameObject log)
    {
        // Check if the log already has a BoxCollider2D
        if (log.GetComponent<BoxCollider2D>() == null)
        {
            log.AddComponent<BoxCollider2D>();
        }

    }
}
