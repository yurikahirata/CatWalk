using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;


public class testCharacterController : MonoBehaviour
{
    public float speed = 6.0F;
    public float turningSpeed = 200f;
    public float jumpSpeed = 30.0f;
    public float gravity = 20.0f;
    public float jumpHeight = 1.25f;
    public LayerMask groundLayer = LayerMask.GetMask();
    private AudioSource source;
    public AudioClip deathSound;

    private Vector3 moveForward = Vector3.zero;

    private float groundRayLength = 0.2f;

    CharacterController controller;

    private float tempJumpHeight;

    private bool rising;

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
    }

    void FixedUpdate()
    {
        // turn around
        if (state == State.Alive)
        {
            float angle = Input.GetAxis("Horizontal") * turningSpeed * Time.deltaTime;
            Quaternion newRot = Quaternion.AngleAxis(angle, transform.up);
            transform.rotation *= newRot;

            //get input to move forward
            moveForward = new Vector3(0, 0, Input.GetAxis("Vertical"));

            // JUMP
            //if is grounded and get button jump, rise into air.
            if (controller.isGrounded)
            {
                if (Input.GetButton("Jump"))
                {
                    moveForward.y += (jumpSpeed * Time.deltaTime);
                    rising = true;
                    tempJumpHeight = transform.position.y + jumpHeight;
                }
            }
            // if is not grounded, check if at top of jump.
            if (!controller.isGrounded)
            {
                float distanceFromJump = Mathf.Abs(tempJumpHeight - transform.position.y);
                //if so, start sinking
                if (distanceFromJump < 0.1)
                {
                    rising = false;
                }
                //rise if haven't reached the top
                if (rising)
                {
                    moveForward.y += jumpSpeed * Time.deltaTime;
                }
                //sink if you have
                else
                {
                    moveForward.y -= gravity * Time.deltaTime;
                }
            }



            // clean up input
            moveForward = transform.TransformDirection(moveForward);
            moveForward *= speed;

            //detect moving platform
            RaycastHit hit;
            bool onPlatform = Physics.Raycast(transform.position, Vector3.down, out hit, groundRayLength, groundLayer);
            if (hit.transform != null)
            {
                Debug.Log("I'm on a " + hit.transform.gameObject);
                bool movingPlat = hit.transform.gameObject.GetComponent<Platform>().moveObj;
                Rigidbody rb = hit.transform.GetComponent<Rigidbody>();
                if (movingPlat) { moveForward += rb.velocity; }
            }

            // actually move
                controller.Move(moveForward * Time.deltaTime);
        }

        if (state == State.Dead)
        {
<<<<<<< Updated upstream
            //source.PlayOneShot(deathSound);
            source.clip = deathSound;
            source.Play();
=======
            SceneManager.LoadScene(SceneManager.GetActiveScene().name);
>>>>>>> Stashed changes
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        
       if (other.gameObject.CompareTag("Respawn"))
        {
            Debug.Log("collided");
            state = State.Dead;
        }
    }
}
