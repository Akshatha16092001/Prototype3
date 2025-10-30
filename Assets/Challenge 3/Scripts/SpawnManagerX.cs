using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpawnManagerX : MonoBehaviour
{
    public GameObject[] objectPrefabs;
    private Vector3 spawnPos = new Vector3(30, 5, 0);
    private float startDelay = 2;
    private float repeatRate = 1.5f;

    private PlayerControllerX playerControllerScript;

    // Start is called before the first frame update
    void Start()
    {
        playerControllerScript = GameObject.Find("Player").GetComponent<PlayerControllerX>();
        InvokeRepeating("SpawnObjects", startDelay, repeatRate);
    }

    // Update is called once per frame
    void Update()
    {

    }

    void SpawnObjects()
    {
        if (playerControllerScript.gameOver == false)
        {
            int index = Random.Range(0, objectPrefabs.Length);
            Vector3 randomSpawnPos = new Vector3(30, Random.Range(5, 15), 0);

            Instantiate(objectPrefabs[index], randomSpawnPos, objectPrefabs[index].transform.rotation);
        }
    }
}
