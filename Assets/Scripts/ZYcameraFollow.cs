using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ZYcameraFollow : MonoBehaviour
{
    public GameObject player;

    // Start is called before the first frame update
    void Start()
    {
        transform.position = new Vector3(transform.position.x, player.transform.position.y + 3, player.transform.position.z - 7);
    }

    // Update is called once per frame
    void Update()
    {
        transform.position = new Vector3(transform.position.x, player.transform.position.y+3, player.transform.position.z -7);
    }
}
