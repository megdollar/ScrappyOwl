using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StarMoveScript : MonoBehaviour
{
    // Speed at which the Star moves to the left
    public float moveSpeed = 5f;

    // Position beyond which the Star is deleted
    public float deadZone = -45f;

    void Start()
    {

    }

    void Update()
    {
        transform.position = transform.position + (Vector3.left * moveSpeed) * Time.deltaTime;

        if (transform.position.x < deadZone)
        {
            // Destroy the Star after the delay
            Destroy(gameObject);
        }
    }
}
