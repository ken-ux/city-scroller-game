using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpawnManager : MonoBehaviour
{
    public GameObject[] obstaclePrefabs;
    private Vector3 spawnPos = new Vector3(25, 0, 0);
    private float startDelay = 2;
    private float repeatRate = 2;
    private PlayerController playerControllerScript;

    // Start is called before the first frame update
    void Start()
    {
        // Spawn obstacles after a delay at a specific rate
        InvokeRepeating("SpawnObstacle", startDelay, repeatRate);

        // Initialize PlayerController to tell when game is over
        playerControllerScript = GameObject.Find("Player").GetComponent<PlayerController>();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    void SpawnObstacle()
    {
        // Randomly pick obstacle to spawn
        int obstacleInt = Random.Range(0, obstaclePrefabs.Length);

        // Spawn obstacle if game is not over
        if (playerControllerScript.gameOver == false)
        {
            GameObject obstaclePrefab = obstaclePrefabs[obstacleInt];
            Instantiate(obstaclePrefab, spawnPos, obstaclePrefab.transform.rotation);
        }
    }
}
