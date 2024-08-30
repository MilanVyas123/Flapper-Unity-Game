using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CloudMoveScript : MonoBehaviour
{
    public float moveSpeed = 1f;
    public float deadZone = -45f; // Adjusted to fit your game's view
    public float spawnDelay = 5f; // Delay before starting to move

    private float spawnTimer;

    // Start is called before the first frame update
    void Start()
    {
        // Initialize the spawn timer
        spawnTimer = spawnDelay;
    }

    // Update is called once per frame
    void Update()
    {
        // Start moving the cloud after the spawn delay
        if (spawnTimer > 0)
        {
            spawnTimer -= Time.deltaTime;
            return; // Skip the movement if the timer is not complete
        }

        // Move the cloud left
        transform.position += Vector3.left * moveSpeed * Time.deltaTime;

        // Check if the cloud has moved past the dead zone
        if (transform.position.x < deadZone)
        {
            Debug.Log("Cloud deleted!");
            Destroy(gameObject);
        }
    }
}
