using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MoveLeft : MonoBehaviour
{
    private float speed = 30.0f;
    private PlayerController playerControllerScript;
    private float leftBound = -15;

    // Start is called before the first frame update
    void Start()
    {
        playerControllerScript = GameObject.Find("Player").GetComponent<PlayerController>();
    }

    // Update is called once per frame
    void Update()
    {
        // Stop GameObjects if they collide into the player
        if (playerControllerScript.gameOver == false)
        {
            if (playerControllerScript.dash == false)
            {
                transform.Translate(Vector3.left * Time.deltaTime * speed);
            } else
            {
                transform.Translate(Vector3.left * Time.deltaTime * speed * 2);
            }
        }

        // Destroy obstacle if it moves past the left boundary
        if (transform.position.x < leftBound && gameObject.CompareTag("Obstacle"))
        {
            Destroy(gameObject);
        }
    }
}