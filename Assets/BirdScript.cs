using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class BirdScript : MonoBehaviour
{
    public Rigidbody2D myRigidBody2D;
    public float flapStrength;
    public LogicScript logic;
    public bool birdIsAlive = true;

    void Start()
    {
        logic = GameObject.FindGameObjectWithTag("Logic").GetComponent<LogicScript>();
    }

    void Update()
    {
        if (birdIsAlive)
        {
            if (Input.GetKeyDown(KeyCode.Space))
            {
                Debug.Log("Space pressed");
                myRigidBody2D.velocity = Vector2.up * flapStrength;
            }

            if (Input.touchCount > 0)
            {
                Touch touch = Input.GetTouch(0);

                if (touch.phase == TouchPhase.Began)
                {
                    Debug.Log("Screen touched");
                    myRigidBody2D.velocity = Vector2.up * flapStrength;
                }
            }

            CheckOutOfBounds();
        }
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        Debug.Log("Collided");
        GameOver();
    }

    private void CheckOutOfBounds()
    {
        // Get the screen bounds in world coordinates
        float screenTop = Camera.main.orthographicSize+10;
        float screenBottom = -screenTop-10;

        // Check if the bird has gone out of bounds
        if (transform.position.y > screenTop || transform.position.y < screenBottom)
        {
            Debug.Log("Bird went out of bounds");
            GameOver();
        }
    }

    private void GameOver()
    {
        logic.gameOver();
        birdIsAlive = false;
    }
}
