using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// https://answers.unity.com/questions/602210/move-a-platform-left-and-right-continuosly.html
public class Platform : MonoBehaviour
{
    /*
    public Transform target; // the target position
    public Transform origin; // the origin position
    */
    public Transform start;
    public Transform end;
    private Rigidbody rb;
    private Vector3 targetPos;
 
    public float speed; // speed - units per second
    public bool moveObj; // toggle motion on and off
    public bool forwards = true;


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
            
            Vector3 currentPos = transform.position;
            float distanceFromEnd = (end.position - currentPos).magnitude;
            float distanceFromStart = (start.position - currentPos).magnitude;
            if (distanceFromEnd < 0.5f)
            {
                targetPos = start.position;
            }
            else if(distanceFromStart < 0.5f)
            {
                targetPos = end.position;
            }

            Vector3 targetDirection = (targetPos - currentPos).normalized;
            rb.MovePosition(transform.position + targetDirection * speed * Time.deltaTime);
            

            /*
            float step = speed * Time.deltaTime; // step size = speed * frame time
            if (forwards == true)
            {
                transform.position = Vector3.MoveTowards(transform.position, target.position, step); // moves position a step closer to the target position
            }
            else
            {
                transform.position = Vector3.MoveTowards(transform.position, origin.position, step); // moves position a step closer to the origin position
            }

            if(transform.position == target.position) { forwards = false; }
            if(transform.position == origin.position) { forwards = true; }
            */
        }
            
            
            
    }
}
