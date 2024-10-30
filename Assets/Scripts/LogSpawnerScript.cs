using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LogSpawnerScript : MonoBehaviour
{
    public GameObject log;
    public float spawnRate = 3f;
    private float timer = 0f;
    public float heightOffset = 6f;

    public float minLogHeight = -5f;
    public float maxLogHeight = 5f;

    private List<GameObject> logs = new List<GameObject>();

    void Start()
    {
        spawnLog();
    }

    void Update()
    {
        if (timer < spawnRate)
        {
            timer += Time.deltaTime;
        }
        else
        {
            spawnLog();
            timer = 0f;
        }
    }

    void spawnLog()
    {
        Camera cam = Camera.main;

        float centerY = cam.transform.position.y;

        float spawnYPosition = Random.Range(centerY + minLogHeight, centerY + maxLogHeight);

        Vector3 spawnPosition = new Vector3(transform.position.x + cam.orthographicSize * cam.aspect, spawnYPosition, 0);

        GameObject newLog = Instantiate(log, spawnPosition, transform.rotation);
        newLog.tag = "Log";
        logs.Add(newLog);

        BoxCollider2D collisionCollider = newLog.AddComponent<BoxCollider2D>();
        collisionCollider.isTrigger = false;
    }

    // Method to destroy all logs
    public void DestroyLogs()
    {
        foreach (GameObject log in logs)
        {
            if (log != null)
            {
                // Destroy the log GameObject
                Destroy(log);
            }
        }
        logs.Clear();
    }

    // Method to hide all logs (make them invisible)
    public void HideTrees()
    {
        foreach (GameObject log in logs)
        {
            if (log != null)
            {
                log.SetActive(false);
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
                log.SetActive(true);
                Debug.Log("Showing log: " + log.name);
            }
        }
    }
}
