using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CarEngine : MonoBehaviour
{
    public Transform _path;
    public List<Transform> nodes;
    public float maxSteerAngle = 45f;
    public WheelCollider wheelFL;
    public WheelCollider wheelFR;
    public float maxMotorTorque = 80f;
    public float currentSpeed;
    public float maxSpeed = 100f;
    public Vector3 centerOfMass;

    private int currentNode = 0;
    private bool avoiding = false;



    [Header("Sensors")]
    public float sensorLength = 4f;
    public Vector3 frontSensorPosition = new Vector3 (0f,0.2f,0.5f);
    public float frontSideSensorPosition = 0.2f;
    public float frontSensorAngle = 30f;



    private void Start()
    {
        GetComponent<Rigidbody>().centerOfMass = centerOfMass;

        Transform[] pathTransform = _path.GetComponentsInChildren<Transform>();
        nodes = new List<Transform>();

        for (int i = 0; i < pathTransform.Length; i++)
        {
            if (pathTransform[i] != _path.transform)
            {
                nodes.Add(pathTransform[i]);
            }
        }
    }

    
    private void FixedUpdate()
    {
        ApplySteer();
        Drive();
        CheckWayPointDistance();
        Sensors();

    }

    private void ApplySteer()
    {
        if (avoiding) return;
        Vector3 relativeVector = transform.InverseTransformPoint(nodes[currentNode].position);
        float newSteer = (relativeVector.x / relativeVector.magnitude)*maxSteerAngle;
        wheelFL.steerAngle = newSteer;
        wheelFR.steerAngle = newSteer;
    }

    private void Drive()
    {
        currentSpeed = 2 * Mathf.PI * wheelFL.radius * wheelFL.rpm * 60 / 100;

        if(currentSpeed < maxSpeed)
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

    private void CheckWayPointDistance()
    {
        if (Vector3.Distance(transform.position, nodes[currentNode].position) < 0.5f)
        {
           
            if(currentNode == nodes.Count - 1)
            {
                Debug.Log(currentNode);
                currentNode = 0;
            }
            else {
                currentNode++;
            }
        }
    }

    private void Sensors()
    {
        RaycastHit hit;
        Vector3 sensorsStartPos = transform.position;
        sensorsStartPos += transform.forward * frontSensorPosition.z;
        sensorsStartPos += transform.up * frontSensorPosition.y;
        float avoidingMultiplier = 0;
        avoiding = false;



        
        //front center sensor
        if (Physics.Raycast(sensorsStartPos,transform.forward,out hit, sensorLength))
        {
            if (hit.collider.CompareTag("RoadBlock"))
            {
                Debug.DrawLine(sensorsStartPos, hit.point);
                avoiding = true;
            } 
            
        }
        
        //front right sensor
        sensorsStartPos += transform.right * frontSideSensorPosition;
        if (Physics.Raycast(sensorsStartPos, transform.forward, out hit, sensorLength))
        {
            if (hit.collider.CompareTag("RoadBlock"))
            {
                Debug.DrawLine(sensorsStartPos, hit.point);
                avoiding = true;
                avoidingMultiplier -= 1f;
            }
        }
        
        //front right angle sensor
        
       else if (Physics.Raycast(sensorsStartPos, transform.forward, out hit, sensorLength))
        {
            if (hit.collider.CompareTag("RoadBlock"))
            {
                Debug.DrawLine(sensorsStartPos, hit.point);
                avoiding = true;
                avoidingMultiplier -= 0.5f;

            }
        }
        
        //front left sensor
        sensorsStartPos -= transform.right * frontSideSensorPosition * 2;
        if (Physics.Raycast(sensorsStartPos, Quaternion.AngleAxis(frontSensorAngle,transform.up)*transform.forward, out hit, sensorLength))
        {
            if (hit.collider.CompareTag("RoadBlock"))
            {
                Debug.DrawLine(sensorsStartPos, hit.point);
                avoiding = true;
                avoidingMultiplier += 1f;
            }
        }
        
        //front left angle sensor
        else if (Physics.Raycast(sensorsStartPos, Quaternion.AngleAxis(-frontSensorAngle, transform.up) * transform.forward, out hit, sensorLength))
        {
            if (hit.collider.CompareTag("RoadBlock"))
            {
                Debug.DrawLine(sensorsStartPos, hit.point);
                avoiding = true;
                avoidingMultiplier += 0.5f;
            }
        }
        if (avoiding)
        {
            wheelFL.steerAngle = maxSteerAngle * avoidingMultiplier;
            wheelFR.steerAngle = maxSteerAngle * avoidingMultiplier;
        }
        

    }


}
