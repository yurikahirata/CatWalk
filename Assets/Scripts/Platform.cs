using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Platform : MonoBehaviour
{
    public Transform target; // the target position
    public Transform origin; // the origin position
    public float speed; // speed - units per second
    public bool moveObj; // toggle motion on and off
    public bool forwards = true;


    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (moveObj == true)
        {
            float step = speed * Time.deltaTime; // step size = speed * frame time
            if (forwards == true)
            {
                transform.position = Vector3.MoveTowards(transform.position, target.position, step); // moves position a step closer to the target position
            }
            else
            {
                transform.position = Vector3.MoveTowards(transform.position, origin.position, step); // moves position a step closer to the target position
            }

            if(transform.position == target.position) { forwards = false; }
            if(transform.position == origin.position) { forwards = true; }

        }
            
            
            
    }
}
