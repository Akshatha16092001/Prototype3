using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerControllerX : MonoBehaviour
{
    public bool gameOver = false;

    [Header("Movement Settings")]
    public float forwardSpeed = 5f;   // moves along X-axis
    public float floatForce = 8f;     // upward boost
    public float hoverHeight = 6f;    // natural float height
    public float hoverSmooth = 2f;    // smoothness of float correction
    public float upperLimit = 15f;    // max height

    private Rigidbody playerRb;

    [Header("Audio & Effects")]
    public ParticleSystem explosionParticle;
    public ParticleSystem fireworksParticle;
    private AudioSource playerAudio;
    public AudioClip moneySound;
    public AudioClip explodeSound;

    void Start()
    {
        playerRb = GetComponent<Rigidbody>();
        playerAudio = GetComponent<AudioSource>();

        // No gravity – we control floating manually
        playerRb.useGravity = false;

        // Start above the ground
        transform.position = new Vector3(0, hoverHeight, 0);
    }

    void Update()
    {
        if (gameOver) return;

        // Constant forward motion (X-axis)
        transform.Translate(Vector3.right * forwardSpeed * Time.deltaTime, Space.World);

        // Maintain floating height automatically
        float heightError = hoverHeight - transform.position.y;
        playerRb.AddForce(Vector3.up * heightError * hoverSmooth, ForceMode.Acceleration);

        // Rise higher when space is held
        if (Input.GetKey(KeyCode.Space) && transform.position.y < upperLimit)
        {
            playerRb.AddForce(Vector3.up * floatForce, ForceMode.Acceleration);
        }
    }

    private void OnCollisionEnter(Collision other)
    {
        if (other.gameObject.CompareTag("Bomb"))
        {
            explosionParticle.Play();
            playerAudio.PlayOneShot(explodeSound, 1.0f);
            gameOver = true;
            Destroy(other.gameObject);
        }
        else if (other.gameObject.CompareTag("Money"))
        {
            fireworksParticle.Play();
            playerAudio.PlayOneShot(moneySound, 1.0f);
            Destroy(other.gameObject);
        }
    }
}
