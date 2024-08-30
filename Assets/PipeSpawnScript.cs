using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PipeSpawnScript : MonoBehaviour
{
    public GameObject pipe;
    public float spawnRate = 7f;
    public float timer = -1f;
    public float heightOffset = 5f; // Adjusted value

    void Start()
    {
        spawnPipe();
    }

    void Update()
    {
        if (timer < spawnRate)
        {
            timer += Time.deltaTime;
        }
        else
        {
            spawnPipe();
            timer = 0f;
        }
    }

    void spawnPipe()
    {
        float lowestPoint = transform.position.y - heightOffset;
        float highestPoint = transform.position.y + heightOffset;
        float spawnYPosition = Random.Range(lowestPoint, highestPoint);

        // Log the Y position for debugging
        Debug.Log("Pipe Spawned at Y Position: " + spawnYPosition);

        Instantiate(pipe, new Vector3(transform.position.x, spawnYPosition, 0f), Quaternion.identity);
    }
}
