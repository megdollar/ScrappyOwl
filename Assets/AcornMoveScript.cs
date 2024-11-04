using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AcornMoveScript : MonoBehaviour
{
    // Speed at which the acorn moves to the left
    public float moveSpeed = 5f;

    // Position beyond which the acorn is deleted
    public float deadZone = -45f;

    void Start()
    {

    }

    void Update()
    {
        transform.position = transform.position + (Vector3.left * moveSpeed) * Time.deltaTime;

        if (transform.position.x < deadZone)
        {
            // Destroy the acorn after the delay
            Destroy(gameObject);
        }
    }
}
