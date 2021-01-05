using UnityEngine;
using System.Collections;

public class SteeringWheel : MonoBehaviour
{

	// Use this for initialization
	public float rotAngle;
	public float rotCoef;

	Transform parentT;

	void Start()
	{
		rotAngle = 0;
		parentT = transform.parent.transform;
	}

	// Update is called once per frame
	void FixedUpdate()
	{

		transform.rotation = parentT.rotation * Quaternion.Euler(0, 0, 90 - rotAngle * rotCoef);

	}

}