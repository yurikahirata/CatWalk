using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CatAnimator : MonoBehaviour
{
    protected Animator animator;
    public float timer;
    public float waitToTurnHead = 5;
    private CharacterController cc;

    // Start is called before the first frame update
    void Start()
    {
        animator = GetComponent<Animator>();
        timer = 0f;
        cc = GetComponent<CharacterController>();
    }

    // Update is called once per frame
    void Update()
    {
        
        // walk when moving
        if (Input.GetAxis("Horizontal") != 0 || Input.GetAxis("Vertical") != 0)
        {
            //Debug.Log("Moving.");
            animator.SetInteger("motion", 1);
            animator.SetBool("turnidle", false);
        }

        //stop moving when there is no input
        if (Input.GetAxis("Horizontal") == 0 && Input.GetAxis("Vertical") == 0)
        {
            //Debug.Log("Stopping.");
            animator.SetInteger("motion", 0);
        }

        //check current state
        AnimatorStateInfo stateInfo = animator.GetCurrentAnimatorStateInfo(0);

        //if current state is Idle_A, go to Idle_B after a while
        if (stateInfo.IsName("Base Layer.Idle_A"))
        {
            timer += Time.deltaTime;
            if(timer > waitToTurnHead)
            {
                animator.SetBool("turnidle", true);
                //timer = 0;
            }
        }

        //if current state is Idle_B, go back to Idle_A when animation is done
        if (stateInfo.IsName("Base Layer.Idle_B"))
        {
            animator.SetBool("turnidle", false);
            timer = 0;
        }

    }
}
