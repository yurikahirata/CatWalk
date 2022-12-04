using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class OurRobotController : MonoBehaviour
{   
    public GameObject cat;
    public float catRange;
    private bool wokeUp;
    
    private Rigidbody rb;
    public Transform start;
    public Transform end;
    public float speed;
    private Vector3 targetPos;
    private OurRobotAnimator myAnim;
    private Animator animator;

    
    // Start is called before the first frame update
    void Start()
    {
        rb = GetComponent<Rigidbody>();
        myAnim = GetComponent<OurRobotAnimator>();
        animator = GetComponent<Animator>();
        wokeUp = false;
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
            WalkAround();
        }
    }

    // Return true if Cat is within catRange distance of self
    bool CatInRange()
    {
        float distance = (cat.transform.position - transform.position).magnitude;
        if(distance < catRange) { return true; }
        return false;
    }

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
            
            //get distance from endpoints
            float distanceFromEnd = (end.position - currentPos).magnitude;
            float distanceFromStart = (start.position - currentPos).magnitude;
            
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
}
