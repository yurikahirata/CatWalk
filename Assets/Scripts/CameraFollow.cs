using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class CameraFollow : MonoBehaviour
{
    public Transform target;
    public float smoothSpeed = 0.125f;
    public Vector3 locationOffset = new Vector3(0f, 1.5f, -3f);
    public Vector3 rotationOffset = Vector3.zero;
    public LayerMask obstacleLayerMask;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        // https://stackoverflow.com/questions/65816546/unity-camera-follows-player-script
        // find desired position based on target, then go there in smooth increments
        Vector3 desiredPosition = target.position + target.rotation * locationOffset;
        Vector3 smoothedPosition = Vector3.Lerp(transform.position, desiredPosition, smoothSpeed);
        transform.position = smoothedPosition;

        Quaternion desiredrotation = target.rotation * Quaternion.Euler(rotationOffset);
        Quaternion smoothedrotation = Quaternion.Lerp(transform.rotation, desiredrotation, smoothSpeed);
        transform.rotation = smoothedrotation;

        // ball roller script
        // check if there's anything between camera + player, slide camera forward if so
        Vector3 targetToCamera = transform.position - target.position;
        RaycastHit hit;
        Vector3 newCameraPosition = transform.position;
        Debug.DrawLine(target.position, newCameraPosition + targetToCamera * 10);
        if (Physics.Raycast(target.position, targetToCamera,
            out hit, locationOffset.magnitude, obstacleLayerMask))
        {
            newCameraPosition = hit.point;
        }
        transform.position = newCameraPosition;



    }
}
