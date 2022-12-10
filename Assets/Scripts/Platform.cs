using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// https://answers.unity.com/questions/602210/move-a-platform-left-and-right-continuosly.html
public class Platform : MonoBehaviour
{
    public Transform start;
    public Transform end;
    private Rigidbody rb;
    private Vector3 targetPos;
 
    public float speed; // speed - units per second
    public bool moveObj; // toggle motion on and off
    // public bool forwards = true;


    // Start is called before the first frame update
    void Start()
    {
        rb = GetComponent<Rigidbody>();
        targetPos = end.position;
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        if (moveObj == true)
        {
            // https://forum.unity.com/threads/move-object-back-and-forth-between-two-points-with-rigidbody.1110332/
            // get current position
            Vector3 currentPos = transform.position;
            
            // get distance from endpoints
            float distanceFromEnd = (end.position - currentPos).magnitude;
            float distanceFromStart = (start.position - currentPos).magnitude;
            
            //if close to an endpoint, change target of walk
            if (distanceFromEnd < 0.5f)
            {
                targetPos = start.position;
            }
            else if(distanceFromStart < 0.5f)
            {
                targetPos = end.position;
            }

            //walk towards target
            Vector3 targetDirection = (targetPos - currentPos).normalized;
            //rb.MovePosition(transform.position + targetDirection * speed * Time.deltaTime);
            transform.position += (targetDirection * speed * Time.deltaTime);
            
        }
            
            
            
    }
}
