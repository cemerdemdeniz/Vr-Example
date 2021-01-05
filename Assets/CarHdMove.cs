using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CarHdMove : MonoBehaviour
{
    public Transform _head;
    bool canMove;
    Rigidbody rigidCar;
    public float carSpeed = 5.0f;
    Vector3 relativeHeadRotation;
    public float currentSpeed = 5.0f;
    public float maxSpeed = 50.0f;
    public float maxMotorTorque = 100.0f;
    public WheelCollider wheelFR;
    public WheelCollider wheelFL;

    void Start()
    {
        rigidCar = GetComponent<Rigidbody>();
        canMove = true;
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        //relativeHeadRotation = _head.transform.rotation.eulerAngles - transform.rotation.eulerAngles;
        _head.transform.rotation.eulerAngles = transform.rotation.eulerAngles;
        AutoPilot();
    }

    void AutoPilot()
    {
        currentSpeed = 2 * Mathf.PI * wheelFL.radius * wheelFL.rpm * 60 / 100;

        if (currentSpeed < maxSpeed)
        {
            wheelFL.motorTorque = maxMotorTorque;
            wheelFR.motorTorque = maxMotorTorque;
        }
        else
        {
            wheelFL.motorTorque = 0;
            wheelFR.motorTorque = 0;
        }
    }
}
