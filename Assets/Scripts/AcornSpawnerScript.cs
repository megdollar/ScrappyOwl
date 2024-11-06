using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class AcornSpawner : MonoBehaviour
{
    public GameObject acorn;
    public float spawnRate = 3;
    private float timer = 0;
    public float heightOffset = 10;
   

    private List<GameObject> spawnedAcorns = new List<GameObject>();
     private bool isGamePaused = false;

    // Start is called before the first frame update
    void Start()
    {
      
    }

    // Update is called once per frame
    void Update()
    {
        if (!isGamePaused)
        {
            timer += Time.deltaTime;
            
            if (timer >= spawnRate)
            {
                spawnObstacle();
                timer = 0f;
            }
        }
    }

    void spawnObstacle()
    {
        Debug.Log("Spawning Acorn");
        float lowestPoint = transform.position.y - heightOffset;
        float highestPoint = transform.position.y + heightOffset;

        GameObject newAcorn = Instantiate(acorn, new Vector3(transform.position.x, Random.Range(lowestPoint, highestPoint), 0), transform.rotation);
        spawnedAcorns.Add(newAcorn);
    }

    // Method to hide acorns on pause
    public void HideAcorns()
    {
        isGamePaused = true;

        foreach (GameObject acorn in spawnedAcorns)
        {
            if (acorn != null)
            {
                acorn.SetActive(false);
            }
        }
    }

       // Method to show acrons on resume
    public void ShowAcorns()
    {
        isGamePaused = false;
        foreach (GameObject acorn in spawnedAcorns)
        {
            if (acorn != null)
            {
                acorn.SetActive(true);
            }
        }
    }

    public void DestroyAcorns()
    {
        foreach (GameObject acorn in spawnedAcorns)
        {
            if (acorn != null)
            {
                Destroy(acorn);
            }
         }

         spawnedAcorns.Clear();
    }
    
}

