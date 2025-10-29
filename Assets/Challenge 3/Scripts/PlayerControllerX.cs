using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerControllerX : MonoBehaviour
{
    public bool gameOver;

    private Rigidbody playerRb;
    public float floatForce;      // How strongly the balloon floats up
    private float gravityModifier = 1.5f;

    public GameObject fireworksParticle; // Fireworks reference

    private float upperLimit = 15f;      // Maximum height limit
    private float lowerLimit = 1f;       // Minimum height limit

    void Start()
    {
        Physics.gravity *= gravityModifier;
        playerRb = GetComponent<Rigidbody>();
    }

    void Update()
    {
        // Let the balloon float up only if game is not over
        if (Input.GetKey(KeyCode.Space) && !gameOver)
        {
            playerRb.AddForce(Vector3.up * floatForce);
        }

        // ✅ Keep the balloon within vertical limits
        if (transform.position.y > upperLimit)
        {
            transform.position = new Vector3(transform.position.x, upperLimit, transform.position.z);
            playerRb.velocity = Vector3.zero; // stop upward movement
        }

        if (transform.position.y < lowerLimit)
        {
            transform.position = new Vector3(transform.position.x, lowerLimit, transform.position.z);
            playerRb.velocity = Vector3.zero; // stop downward movement
        }
    }

    private void OnCollisionEnter(Collision other)
    {
        // When balloon hits the ground → game over
        if (other.gameObject.CompareTag("Ground"))
        {
            gameOver = true;
            Debug.Log("Game Over!");

            // 🎆 Show fireworks at balloon position
            fireworksParticle.transform.position = transform.position;
            fireworksParticle.SetActive(true);
        }
    }
}
