using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class testCharacterController : MonoBehaviour
{
    public float speed = 6.0F;
    public float turningSpeed = 3f;
    public float jumpSpeed = 8.0F;
    public float gravity = 20.0F;

    private Vector3 moveDirection = Vector3.zero;


    CharacterController controller;
    void Start()
    {
        controller = GetComponent<CharacterController>();
    }

    void FixedUpdate()
    {
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
            var step = turningSpeed * Time.deltaTime;
            Quaternion newRotation = Quaternion.LookRotation(moveDirection, transform.up);
            transform.rotation = Quaternion.Slerp(transform.rotation, newRotation, step);
        }


        moveDirection.y -= gravity * Time.deltaTime;
        controller.Move(moveDirection * Time.deltaTime);

    }
}
