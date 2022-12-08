using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class OurRobotController : MonoBehaviour
{   
    public GameObject cat;
    public float catRange;
    public Transform start;
    public Transform end;
    public float speed;

    private bool wokeUp;
    private Rigidbody rb;
    private Vector3 targetPos;
    private OurRobotAnimator myAnim;
    private Animator animator;

    private Vector3 toEnd;
    private Vector3 fromEnd;
    private bool forward;

    
    // Start is called before the first frame update
    void Start()
    {
        rb = GetComponent<Rigidbody>();
        myAnim = GetComponent<OurRobotAnimator>();
        animator = GetComponent<Animator>();
        wokeUp = false;
        targetPos = end.position;

        forward = true;
        toEnd = (end.position - start.position).normalized;
        fromEnd = (start.position - end.position).normalized;
    }

    // Update is called once per frame
    void FixedUpdate()
    {

        //if cat is in range, wake up
        if (CatInRange())
        {
            myAnim.Wake();
            wokeUp = true;
        }
        //once awake, walk around
        if (wokeUp)
        {
            //WalkAround();
            
            Walk();
        }
    }

    // Return true if Cat is within catRange distance of self
    bool CatInRange()
    {
        float distance = (cat.transform.position - transform.position).magnitude;
        if(distance < catRange) { return true; }
        return false;
    }

    // Walk towards an endpoint
    void Walk()
    {
        //transition to walking animation
        myAnim.StartWalk();

        //once walking animation starts
        AnimatorStateInfo state = animator.GetCurrentAnimatorStateInfo(0);
        if (state.IsName("anim_Walk_Loop"))
        {
            CheckForEndpoint();
            if (forward)
            {
                rb.MovePosition(transform.position + toEnd * speed * Time.deltaTime);
            }
            else if (!forward)
            {
                rb.MovePosition(transform.position + fromEnd * speed * Time.deltaTime);
            }
        }

    }

    //new switching direction function
    void SwitchDirection()
    {
        forward = !forward;
        Debug.Log("Time is " + Time.deltaTime + " and I am now going forward? " + forward);
    }

    //check if close to an endpoint, switch direction
    void CheckForEndpoint()
    {
        //get distance from endpoints, not counting height
        Vector3 endV = end.position;
        endV.y = 0;
        Vector3 startV = start.position;
        startV.y = 0;
        Vector3 curV = transform.position;
        curV.y = 0;
        float distanceFromEnd = Vector3.Distance(endV, curV);
        float distanceFromStart = Vector3.Distance(startV, curV);

        //if close to an endpoint, change target of walk
        if (distanceFromEnd < 0.5f)
        {
            //SwitchDirection();
            forward = false;
        }
        if (distanceFromStart < 0.5f)
        {
            //SwitchDirection();
            forward = true;
        }
    }

    /*
    private void OnCollisionEnter(Collision other)
    {
        for(int i = 0; i < other.contactCount; i++)
        {
            Vector3 incomingDir = transform.position - other.GetContact(i).point;
            if(incomingDir == toEnd) { Debug.Log("Something came at me from toEnd");  forward = false; }
            if (incomingDir == fromEnd) { Debug.Log("Something came at me from fromEnd");  forward = true; }
        }
        //SwitchDirection();
    }
    */


    /*
    // Walk around from Start to End
    void WalkAround()
    {
        // https://forum.unity.com/threads/move-object-back-and-forth-between-two-points-with-rigidbody.1110332/
        //transition to walking animation
        myAnim.StartWalk();
        
        //once walking animation starts
        AnimatorStateInfo state = animator.GetCurrentAnimatorStateInfo(0);
        if(state.IsName("anim_Walk_Loop"))
        {
            //get current position
            Vector3 currentPos = transform.position;

            //get distance from endpoints, not counting height
            Vector3 endV = end.position;
            endV.y = 0;
            Vector3 startV = start.position;
            startV.y = 0;
            Vector3 curV = transform.position;
            curV.y = 0;
            float distanceFromEnd = Vector3.Distance(endV, curV);
            float distanceFromStart = Vector3.Distance(startV, curV);

            //if close to an endpoint, change target of walk
            if (distanceFromEnd < 0.5f)
            {
                targetPos = start.position;
            }
            else if (distanceFromStart < 0.5f)
            {
                targetPos = end.position;
            }
            //walk towards target
            Vector3 targetDirection = (targetPos - currentPos).normalized;
            rb.MovePosition(transform.position + targetDirection * speed * Time.deltaTime);
        }   

        }
    */
}
