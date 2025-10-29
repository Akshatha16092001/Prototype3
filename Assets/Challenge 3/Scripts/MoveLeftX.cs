using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MoveLeftX : MonoBehaviour
{
    private float speed = 10;
    private float leftBound = -15;
    private PlayerControllerX playerControllerScript; // Note: PlayerControllerX for the X-version game

    // Start is called before the first frame update
    void Start()
    {
        // Get the PlayerControllerX script from the Player GameObject
        playerControllerScript = GameObject.Find("Player").GetComponent<PlayerControllerX>();
    }

    // Update is called once per frame
    void Update()
    {
        // ✅ Background (and any other object) moves left while game is NOT over
        if (playerControllerScript.gameOver == false)
        {
            transform.Translate(Vector3.left * Time.deltaTime * speed);
        }

        // ✅ Destroy obstacles that go out of bounds
        if (transform.position.x < leftBound && gameObject.CompareTag("Obstacle"))
        {
            Destroy(gameObject);
        }
    }
}
