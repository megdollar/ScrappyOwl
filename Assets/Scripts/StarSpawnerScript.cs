using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class StarSpawner : MonoBehaviour
{
    public GameObject star;
    public float spawnRate = 12;
    private float timer = 0;
    public float heightOffset = 5;

    // List to keep track of spawned stars
    private List<GameObject> spawnedStars = new List<GameObject>();

    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        if (timer < spawnRate)
        {
            timer += Time.deltaTime;
        }
        else
        {
            spawnObstacle();
            timer = 0;
        }
    }

    void spawnObstacle()
    {
        float lowestPoint = transform.position.y - heightOffset;
        float highestPoint = transform.position.y + heightOffset;

        GameObject newStar = Instantiate(star, new Vector3(transform.position.x, Random.Range(lowestPoint, highestPoint), 0), transform.rotation);
        spawnedStars.Add(newStar);
    }

    // Method to destroy all spawned stars
    public void DestroyStars()
    {
        foreach (GameObject star in spawnedStars)
        {
            if (star != null)
            {
                Destroy(star);
            }
        }
        // Clear the list after reset
        spawnedStars.Clear();
    }
}
