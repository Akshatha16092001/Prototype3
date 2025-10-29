using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpawnManagerX : MonoBehaviour
{
    public GameObject[] objectPrefabs;
    private float spawnDelay = 2f;
    private float spawnInterval = 1.5f;

    private PlayerControllerX playerControllerScript;

    void Start()
    {
        playerControllerScript = GameObject.Find("Player").GetComponent<PlayerControllerX>();
        InvokeRepeating("SpawnObjects", spawnDelay, spawnInterval);
    }

    void SpawnObjects()
    {
        // Random position on right side, random height
        Vector3 spawnLocation = new Vector3(30, Random.Range(5, 15), 0);

        int index = Random.Range(0, objectPrefabs.Length);

        if (!playerControllerScript.gameOver)
        {
            // ✅ Always spawn upright (no prefab rotation)
            GameObject spawnedObject = Instantiate(objectPrefabs[index], spawnLocation, Quaternion.identity);

            // ✅ Force reset any local rotation (in case prefab has rotation)
            spawnedObject.transform.rotation = Quaternion.identity;

            // ✅ Remove any animator that might be rotating it
            Animator anim = spawnedObject.GetComponent<Animator>();
            if (anim != null)
            {
                Destroy(anim);
            }

            // ✅ Disable any rotation script if it exists
            MonoBehaviour[] scripts = spawnedObject.GetComponents<MonoBehaviour>();
            foreach (MonoBehaviour script in scripts)
            {
                if (script != this && script.enabled && script.GetType().Name.ToLower().Contains("rotate"))
                {
                    script.enabled = false;
                }
            }

            // ✅ Lock rigidbody motion completely
            Rigidbody rb = spawnedObject.GetComponent<Rigidbody>();
            if (rb != null)
            {
                rb.useGravity = false;
                rb.velocity = Vector3.zero;
                rb.angularVelocity = Vector3.zero;
                rb.constraints = RigidbodyConstraints.FreezeAll;
            }
        }
    }
}
