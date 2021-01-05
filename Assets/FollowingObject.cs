using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FollowingObject : MonoBehaviour
{
    public float smoothness;
    public Transform targetCarObject;
    private Vector3 initialOffSet;
    private Vector3 camPos;

     
    void Start()
    {
        initialOffSet = transform.position - targetCarObject.position;
    }

    
    void FixedUpdate()
    {
        camPos = targetCarObject.position + initialOffSet;
        transform.position = Vector3.Lerp(transform.position, camPos, smoothness * Time.deltaTime);
    }
}
