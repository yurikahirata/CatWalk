using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class testCharacterController : MonoBehaviour
{
    public float speed = 6.0F;
    public float turningSpeed = 3f;
    public float jumpSpeed = 8.0F;
    public float gravity = 20.0F;

    private Vector3 moveForward = Vector3.zero;

    //protected Platform onMovingPlatform;
    public LayerMask groundLayer = LayerMask.GetMask();
    public float groundRayLength = 0.2f;


    CharacterController controller;
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
        if (controller.isGrounded)
        {
            if (Input.GetButton("Jump"))
                moveForward.y = jumpSpeed;
        }
        

        //detect moving platform
        RaycastHit hit;
        bool onPlatform = Physics.Raycast(transform.position, Vector3.down, out hit, groundRayLength, groundLayer);
        if (hit.transform != null)
        {
            Debug.Log("I'm on a platform!");
            Vector3 targetPos = hit.transform.parent.GetComponent<Platform>().target.position;
            Vector3 originPos = hit.transform.parent.GetComponent<Platform>().origin.position;
            float platSpeed = hit.transform.parent.GetComponent<Platform>().speed;
            Vector3 platformVel = (targetPos - originPos) * platSpeed;
            moveForward += platformVel;
        }

        // move forward
        moveForward = transform.TransformDirection(moveForward);
        moveForward *= speed;
        moveForward.y -= gravity * Time.deltaTime;
        controller.Move(moveForward * Time.deltaTime);

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
