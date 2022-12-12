using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class catController : MonoBehaviour
{
    public float speed = 6.0F;
    public float turningSpeed = 200f;
    public LayerMask groundLayer = LayerMask.GetMask();
    private AudioSource source;
    public AudioClip deathSound;

    private Vector3 playerVelocity;
    private Vector3 moveForward;

    private float groundRayLength = 0.2f;

    CharacterController controller;

    private float jumpGravity;
    private float groundedTimer;
    public float jumpHeight = 1.25f;


    public enum State
    {
        Alive,
        Dead,
    }

    private State state;


    void Start()
    {
        source = GetComponent<AudioSource>();
        controller = GetComponent<CharacterController>();
        state = State.Alive;
        jumpGravity = -9.81f;
        groundedTimer = 0;
    }

    void FixedUpdate()
    {
        if (PauseMenuScript.isPaused) return;

        if (state == State.Alive)
        {
            if (controller.isGrounded && playerVelocity.y < 0)
            {
                playerVelocity.y = 0f;
            }

            // turn around
            float angle = Input.GetAxis("Horizontal") * turningSpeed * Time.deltaTime;
            Quaternion newRot = Quaternion.AngleAxis(angle, transform.up);
            transform.rotation *= newRot;

            //get input to move forward
            moveForward = new Vector3(0, 0, Input.GetAxis("Vertical"));

            // clean up input
            moveForward = transform.TransformDirection(moveForward);
            moveForward *= speed;

            //detect moving platform
            RaycastHit hit;
            bool onPlatform = Physics.Raycast(transform.position, Vector3.down, out hit, groundRayLength, groundLayer);
            if (hit.transform != null)
            {
                bool movingPlat = hit.transform.gameObject.GetComponent<Platform>().moveObj;
                Rigidbody rb = hit.transform.GetComponent<Rigidbody>();
                if (movingPlat) { moveForward += rb.velocity; }
            }

            // actually move
            controller.Move(moveForward * Time.deltaTime);

            // JUMP
            groundedTimer -= Time.deltaTime;

            if (Input.GetKey(KeyCode.Space) && groundedTimer <= 0.0f)
            {
                playerVelocity.y += Mathf.Sqrt(jumpHeight * -3.0f * jumpGravity);
                groundedTimer = 1.4f;
            }

            playerVelocity.y += jumpGravity * Time.deltaTime;
            controller.Move(playerVelocity * Time.deltaTime);


        }


        if (state == State.Dead)
        {
            StartCoroutine(OnDeath());
            SceneManager.LoadScene(SceneManager.GetActiveScene().name);
        }
    }

    private void OnTriggerEnter(Collider other)
    {

        if (other.gameObject.CompareTag("Respawn"))
        {
            state = State.Dead;
        }
    }

    private IEnumerator OnDeath()
    {
        source.clip = deathSound;
        source.Play();
        yield return new WaitForSeconds(0);
    }
}
