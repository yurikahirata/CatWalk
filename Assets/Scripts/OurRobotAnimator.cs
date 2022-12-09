using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class OurRobotAnimator : MonoBehaviour
{
    private Vector3 rot = Vector3.zero;
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
    }

    public void StartWalk()
    {
        anim.SetBool("Walk_Anim", true);
    }

    public void Idle()
    {
        anim.SetBool("Walk_Anim", false);
    }

    public void Wake()
    {
        anim.SetBool("Open_Anim", true);
    }

    public void Sleep()
    {
        anim.SetBool("Open_Anim", false);
    }


}
