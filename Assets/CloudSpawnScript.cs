using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CloudSpawnScript : MonoBehaviour
{
    public GameObject cloud;  // Reference to the cloud prefab
    public float spawnRate = 10f;  // Set spawn rate to 5 seconds
    private float timer = 0f;  // Initialize timer to 0
    public float heightOffset = 5f; // Offset for random vertical spawning

    void Start()
    {
        // Start the spawn process
        spawnCloud();
    }

    void Update()
    {
        // Increment the timer
        timer += Time.deltaTime;

        // Check if the timer exceeds the spawn rate
        if (timer >= spawnRate)
        {
            spawnCloud(); // Call the spawn function
            timer = 0f;   // Reset the timer
        }
    }

    void spawnCloud()
    {
        // Calculate the lowest and highest points for cloud spawning
        float lowestPoint = transform.position.y - heightOffset;
        float highestPoint = transform.position.y + heightOffset;

        // Generate a random Y position within the range
        float spawnYPosition = Random.Range(lowestPoint, highestPoint);

        // Log the Y position for debugging
        Debug.Log("Cloud spawned at Y Position: " + spawnYPosition);

        // Instantiate the cloud at the calculated position
        Instantiate(cloud, new Vector3(transform.position.x, spawnYPosition, 0f), Quaternion.identity);
    }
}
