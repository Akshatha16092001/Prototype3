using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerControllerX : MonoBehaviour
{
    public bool gameOver;

    private Rigidbody playerRb;
    public float floatForce;
    private float gravityModifier = 1.5f;

    public GameObject fireworksParticle; // Fireworks object

    // Limits for balloon height
    private float upperLimit = 14.5f;
    private float lowerLimit = 1.0f;

    void Start()
    {
        Physics.gravity *= gravityModifier;
        playerRb = GetComponent<Rigidbody>();
    }

    void Update()
    {
        // Float up if space is pressed and game not over
        if (Input.GetKey(KeyCode.Space) && !gameOver)
        {
            playerRb.AddForce(Vector3.up * floatForce);
        }

        // ✅ Keep balloon from floating too high
        if (transform.position.y > upperLimit)
        {
            transform.position = new Vector3(transform.position.x, upperLimit, transform.position.z);
            playerRb.velocity = Vector3.zero; // Stop upward motion
        }

        // ✅ Keep balloon from falling off the screen
        if (transform.position.y < lowerLimit)
        {
            transform.position = new Vector3(transform.position.x, lowerLimit, transform.position.z);
            playerRb.velocity = Vector3.zero; // Stop downward motion
        }
    }

    private void OnCollisionEnter(Collision other)
    {
        // When balloon hits the ground
        if (other.gameObject.CompareTag("Ground"))
        {
            gameOver = true;
            Debug.Log("GAME OVER!");

            // Fireworks appear at balloon’s position
            fireworksParticle.transform.position = transform.position;
            fireworksParticle.SetActive(true);
        }
    }
}
