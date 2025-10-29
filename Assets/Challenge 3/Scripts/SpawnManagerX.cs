using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpawnManagerX : MonoBehaviour
{
    public GameObject[] objectPrefabs;
    private float spawnDelay = 2f;
    private float spawnInterval = 1.5f;

    private PlayerControllerX playerControllerScript;

    // Start is called before the first frame update
    void Start()
    {
        // Get reference to PlayerControllerX script
        playerControllerScript = GameObject.Find("Player").GetComponent<PlayerControllerX>();

        // ✅ Make sure the method name matches exactly
        InvokeRepeating("SpawnObjects", spawnDelay, spawnInterval);
    }

    // ✅ Spawns random bombs or money objects while the game is active
    void SpawnObjects()
    {
        // Random spawn position (right side, random height)
        Vector3 spawnLocation = new Vector3(30, Random.Range(5, 15), 0);

        // Choose a random prefab from the array
        int index = Random.Range(0, objectPrefabs.Length);

        // Spawn only if the game is still active
        if (!playerControllerScript.gameOver)
        {
            Instantiate(objectPrefabs[index], spawnLocation, objectPrefabs[index].transform.rotation);
        }
    }
}
