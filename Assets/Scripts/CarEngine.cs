using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CarEngine : MonoBehaviour
{
    public Transform _path;
    private List<Transform> nodes;
    public float maxSteerAngle = 45f;
    public WheelCollider wheelFL;
    public WheelCollider wheelFR;

    private int currentNode = 0;
    private void Start()
    {
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
    }

    private void ApplySteer()
    {
        Vector3 relativeVector = transform.InverseTransformPoint(nodes[currentNode].position);
        float newSteer = (relativeVector.x / relativeVector.magnitude)*maxSteerAngle;
        wheelFL.steerAngle = newSteer;
        wheelFR.steerAngle = newSteer;
    }


}
