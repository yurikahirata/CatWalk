using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpotlightScript : MonoBehaviour
{

    public float speed;
    private float swingMax; // maximum angle
    private float swingMin; // minimum angle
    private float direction; // forward or backwards

<<<<<<< Updated upstream
=======
    public enum State
    {
        swingFull,
        swingHalf,
        swingNone,
    }

    public State state;
  
>>>>>>> Stashed changes

    void Start()
    {
        direction = 1;
    }

    void Update()
    {
        if (state != State.swingNone)
        {
            if (state == State.swingFull) // if swinging
            {
                swingMax = 0.9f;
                swingMin = 0.5f; // approx. 60 degrees
            }

            if (state == State.swingHalf)
            {
                swingMax = 0.9f; // approx. 120 degrees
                swingMin = 0.75f;
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
