using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class testCharacterController : MonoBehaviour
{
    public float speed = 6.0F;
    public float turningSpeed = 200f;
    public float jumpSpeed = 30.0f;
    public float gravity = 20.0f;
    public float jumpHeight = 1.25f;
    public LayerMask groundLayer = LayerMask.GetMask();

    private Vector3 moveForward = Vector3.zero;

    private float groundRayLength = 0.2f;

    CharacterController controller;

    private bool rising;
    
    private float tempJumpHeight;

    void Start()
    {
        controller = GetComponent<CharacterController>();
    }

    void FixedUpdate()
    {
        // turn around
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
            if(distanceFromJump < 0.1)
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


        /*
        if (controller.isGrounded)
        {
            if (Input.GetButton("Jump"))
            {
                //moveForward.y = jumpSpeed;
                moveForward.y += Mathf.Sqrt(jumpSpeed);
            }

        }
        */

        // clean up input
        moveForward = transform.TransformDirection(moveForward);
        moveForward *= speed;
        //moveForward.y -= gravity * Time.deltaTime;

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

        /*
        // move forward
        moveForward = transform.TransformDirection(moveForward);
        moveForward *= speed;
        moveForward.y -= gravity * Time.deltaTime;
        controller.Move(moveForward * Time.deltaTime);
        */




        /*
        // if controller is grounded: get move direction and speed
        if (controller.isGrounded)
        {
            
            moveDirection = new Vector3(Input.GetAxis("Horizontal"), 0, Input.GetAxis("Vertical"));
            moveDirection = transform.TransformDirection(moveDirection);
            moveDirection *= speed;
            if (Input.GetButton("Jump"))
                moveDirection.y = jumpSpeed;
        }

        //issues with going backwards
        if(moveDirection != Vector3.zero)
        {
            var step = turningSpeed * Time.deltaTime; //angle = this * input on horizontal axis; turning on y-axis (transform.up)
            Vector3 newLook = moveDirection;
            newLook.y = 0;
            Quaternion newRotation = Quaternion.LookRotation(newLook, transform.up);
            transform.rotation = newRotation; // Quaternion.Slerp(transform.rotation, newRotation, step);
        }


        moveDirection.y -= gravity * Time.deltaTime;
        controller.Move(moveDirection * Time.deltaTime);

        //RaycastHit hit = Physics.Raycast(groundCheck.transform.position, Vector3.down, groundRayLength, groundLayers);
        //if (hit.collider!=null)
        //{
        //    onMovingPlatform = hit.collider.gameObject.GetComponent<MovingPlatform>();
        //}
        */
    }
}
