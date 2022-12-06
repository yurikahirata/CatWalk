using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpotlightScript : MonoBehaviour
{

    public bool swingFull;
    public bool swingHalf;
    public float speed;
    private float swingMax; // maximum angle
    private float swingMin; // minimum angle
    private float direction; // forward or background

    void Start()
    {
        direction = 1;
    }

    void Update()
    {
        if (swingFull || swingHalf) // if swinging
        {
            if (swingHalf)
            {
                swingMax = 0.9f; // approx. 120 degrees
                swingMin = 0.75f; // approx. 90 degrees
            } else
            {
                swingMax = 0.9f;
                swingMin = 0.5f; // approx. 60 degrees
            }

            if (transform.rotation.x > swingMax)
            {
                direction = -1;
            }


            if (transform.rotation.x < swingMin)
                direction = 1;

            transform.Rotate(Time.deltaTime * speed * direction, 0, 0);
        }
    }
}
