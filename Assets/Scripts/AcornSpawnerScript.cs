using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AcornSpawner : MonoBehaviour
{
    public GameObject acorn;
    public float spawnRate = 3;
    private float timer = 0;
    public float heightOffset = 10;

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

        Instantiate(acorn, new Vector3(transform.position.x, Random.Range(lowestPoint, highestPoint), 0), transform.rotation);

    }
}
