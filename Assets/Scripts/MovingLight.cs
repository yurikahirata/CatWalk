using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MovingLight : MonoBehaviour
{
    public bool isMoving;
    public float timer;
    public float speed;
    public Vector3 direction;

    private Rigidbody rbd; 
    private float timeToChangeDir;

    // Start is called before the first frame update
    void Start()
    {
        rbd = GetComponent<Rigidbody>();
        timeToChangeDir = 0;
    }

    // Update is called once per frame
    void Update()
    {
        Move();
    }

    public void Move()
    {
        if (isMoving)
        {
            timeToChangeDir -= Time.deltaTime;
            if (timeToChangeDir < 0) { ChangeDirection(); }
            //rbd.velocity = transform.forward * speed;
            rbd.MovePosition(transform.position + direction * speed * Time.deltaTime);
        }
        else if (!isMoving)
        {
            rbd.velocity = new Vector3(0, 0, 0);
        }
    }

    // change direction when necessary
    public void ChangeDirection()
    {
        //get a new direction
        float angle = Random.Range(0f, 360f);
        Quaternion quat = Quaternion.AngleAxis(angle, Vector3.up);
        //get newDir
        direction = quat * Vector3.forward;
        direction.y = 0;
        direction.Normalize();
        //set up time again
        timeToChangeDir = timer;
    }

    private void OnCollisionEnter(Collision collision)
    {
        // get direction from which you were collided
        Vector3 cp = collision.contacts[0].point;
        Vector3 dir = cp - transform.position;
        // go in other direction
        direction = -dir;
    }


}
