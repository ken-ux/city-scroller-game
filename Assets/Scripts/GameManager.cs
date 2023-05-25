using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    // For determining length of time for starting animation
    private float introductionTime = 2.0f;
    private float elapsedTime;

    // For pausing animations until the player enters the scene
    private GameObject player;
    private GameObject background;
    private GameObject spawnManager;
    private bool isGameStarted;
    private Vector3 startPos;
    private Vector3 finalPos = new Vector3(0, 0, 0);

    // For keeping track of score
    private float score = 0;
    private PlayerController playerControllerScript;

    // Start is called before the first frame update
    void Start()
    {
        player = GameObject.Find("Player");
        background = GameObject.Find("Background");
        spawnManager = GameObject.Find("SpawnManager");
        playerControllerScript = player.GetComponent<PlayerController>();
        startPos = player.transform.position;
    }

    // Update is called once per frame
    void Update()
    {
        if (!isGameStarted)
        {
            StartingAnimation();
        } else
        {
            if (playerControllerScript.gameOver == false)
            {
                if (playerControllerScript.dash == false)
                {
                    score += Time.deltaTime;
                }
                else
                {
                    score += Time.deltaTime * 2;
                }
                Debug.Log("Score = " + Mathf.RoundToInt(score));
            }
        }
    }

    void StartingAnimation()
    {
        // Disable background movement and obstacle spawning during intro
        background.GetComponent<MoveLeft>().enabled = false;
        spawnManager.GetComponent<SpawnManager>().enabled = false;

        // Move player into scene
        elapsedTime += Time.deltaTime;
        player.transform.position = Vector3.Lerp(startPos, finalPos, elapsedTime / introductionTime);

        // Enable background movement and obstacle spawning
        if (elapsedTime > introductionTime)
        {
            background.GetComponent<MoveLeft>().enabled = true;
            spawnManager.GetComponent<SpawnManager>().enabled = true;
            isGameStarted = true;
        }
    }
}
