using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    // For making the player jump and animate
    private Rigidbody playerRb;
    private Animator playerAnim;
    public float jumpForce;
    public float gravityModifier;

    // Limiting number of jumps or determining game over
    public bool isOnGround = true;
    public bool gameOver = false;
    private bool doubleJumped = false;

    // Particle and sound effects when running or obstacle is hit
    public ParticleSystem dirtParticle;
    public ParticleSystem explosionParticle;
    public AudioClip jumpSound;
    public AudioClip crashSound;

    // Music and determining dash mode
    private AudioSource playerAudio;
    public bool dash = false;
    private GameManager gameManager;

    // Start is called before the first frame update
    void Start()
    {
        // Initialize components
        playerRb = GetComponent<Rigidbody>();
        playerAnim = GetComponent<Animator>();
        playerAudio = GetComponent<AudioSource>();
        gameManager = GameObject.FindAnyObjectByType<GameManager>();

        // Increase gravity for game
        Physics.gravity *= gravityModifier;
    }

    // Update is called once per frame
    void Update()
    {
        // Allow key inputs only if game isn't over
        if (!gameOver)
        {
            // Player jumps if they're on the ground
            if (Input.GetKeyDown(KeyCode.Space) && isOnGround)
            {
                playerRb.AddForce(Vector3.up * jumpForce, ForceMode.Impulse);
                isOnGround = false;
                playerAnim.SetTrigger("Jump_trig");
                dirtParticle.Stop();
                playerAudio.PlayOneShot(jumpSound, 1.0f);
            }
            // Player can double jump if they haven't already
            else if (Input.GetKeyDown(KeyCode.Space) && !doubleJumped)
            {
                playerRb.AddForce(Vector3.up * jumpForce, ForceMode.Impulse);
                playerAnim.SetTrigger("Jump_trig");
                playerAudio.PlayOneShot(jumpSound, 1.0f);
                doubleJumped = true;
            }

            // Speed up player animation while holding Q
            if (Input.GetKey(KeyCode.Q))
            {
                dash = true;
                playerAnim.speed = 2;
            }
            else
            {
                dash = false;
                playerAnim.speed = 1;
            }
        }
    }

    private void OnCollisionEnter(Collision collision)
    {
        // Kick up dirt when running on ground
        if (collision.gameObject.CompareTag("Ground"))
        {
            isOnGround = true;
            dirtParticle.Play();
            doubleJumped = false;
        }
        // End game if player hits obstacle
        else if (collision.gameObject.CompareTag("Obstacle"))
        {
            gameOver = true;
            Debug.Log("Game Over!");
            playerAnim.SetBool("Death_b", true);
            playerAnim.SetInteger("DeathType_int", 1);
            explosionParticle.Play();
            dirtParticle.Stop();
            playerAudio.PlayOneShot(crashSound, 1.0f);
            gameManager.restartButton.gameObject.SetActive(true);
            gameManager.gameOverText.gameObject.SetActive(true);
        }
    }
}
