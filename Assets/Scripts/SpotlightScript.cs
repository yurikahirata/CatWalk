using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpotlightScript : MonoBehaviour
{

    public bool swingFull;
    public bool swingHalf;
    public float speed;
    private float swingMax;
    private float swingMin;
    private float direction;

    void Start()
    {
        swingMax = 0.9f;
        swingMin = 0.5f;
        direction = 1;
    }

    void Update()
    {
        if (swingFull || swingHalf)
        {
            if (swingHalf)
            {
                swingMax = 0.9f;
                swingMin = 0.75f;
            } else
            {
                swingMax = 0.9f;
                swingMin = 0.5f;
            }

            if (transform.rotation.x > swingMax)
            {
                direction = -1;
                Debug.Log("Changed directions");
            }


            if (transform.rotation.x < swingMin)
                direction = 1;

            transform.Rotate(Time.deltaTime * speed * direction, 0, 0);
        }
    }
}
