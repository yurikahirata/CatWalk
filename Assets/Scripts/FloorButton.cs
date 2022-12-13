using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FloorButton : MonoBehaviour
{
    public GameObject platform;
    public bool ifLevel4;

    // Start is called before the first frame update
    void Start()
    {
        platform.SetActive(false);
        if (ifLevel4)
            gameObject.GetComponent<Renderer>().material.color = new Color(0.3f,0.3f,0.3f);

    }

    // Update is called once per frame
    void Update()
    {
     
    }

    private void OnTriggerStay(Collider other)
    {
        if (other.gameObject.CompareTag("Player") || other.gameObject.CompareTag("Trigger"))
        {
            platform.SetActive(true);
            gameObject.GetComponent<Renderer>().material.color = Color.green;
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.gameObject.CompareTag("Player") || other.gameObject.CompareTag("Trigger"))
        {
            platform.SetActive(false);
            if (ifLevel4)
                gameObject.GetComponent<Renderer>().material.color = new Color(0.3f, 0.3f, 0.3f);
            else
                gameObject.GetComponent<Renderer>().material.color = Color.red;
        }
    }
}
