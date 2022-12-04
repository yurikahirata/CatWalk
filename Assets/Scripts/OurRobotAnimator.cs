using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class OurRobotAnimator : MonoBehaviour
{
    private Vector3 rot = Vector3.zero;
    private float rotSpeed = 40f;
    private Animator anim;
    

    // Use this for initialization
    void Awake()
    {
        anim = gameObject.GetComponent<Animator>();
        gameObject.transform.eulerAngles = rot;
        anim.SetBool("Open_Anim", false);

    }

    // Update is called once per frame
    void Update()
    {
        gameObject.transform.eulerAngles = rot;

        
    }

    public void StartWalk()
    {
        anim.SetBool("Walk_Anim", true);
    }

    public void Idle()
    {
        anim.SetBool("Walk_Anim", false);
    }

    /*
    public void WakeUp()
    {
        if (!anim.GetBool("Open_Anim"))
        {
            anim.SetBool("Open_Anim", true);
        }
        else
        {
            anim.SetBool("Open_Anim", false);
        }
    }
    */

    public void Wake()
    {
        anim.SetBool("Open_Anim", true);
    }

    public void Sleep()
    {
        anim.SetBool("Open_Anim", false);
    }


}
