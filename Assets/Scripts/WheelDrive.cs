using UnityEngine;
using System;





[Serializable]
public enum DriveType
{
	RearWheelDrive,
	FrontWheelDrive,
	AllWheelDrive
}




public class WheelDrive : MonoBehaviour
{
	public float currentSpeed = 5.0f;
	public float maxSpeed = 100f;
	public WheelCollider wheelFL;
	public WheelCollider wheelFR;
	//private float yaw = 0.0f;
	//private float pitch = 0.0f;
	public float speedH = 2.0f;
	public float speedV = 2.0f;
	public GameObject playerCam;
	private Vector3 movingDirection = Vector3.zero;
	




	[Tooltip("Maximum steering angle of the wheels")]
	public float maxAngle = 30f;
	[Tooltip("Maximum torque applied to the driving wheels")]
	public float maxMotorTorque = 300f;
	[Tooltip("Maximum brake torque applied to the driving wheels")]
	public float brakeTorque = 30000f;
	[Tooltip("If you need the visual wheels to be attached automatically, drag the wheel shape here.")]
	public GameObject wheelShape;

	[Tooltip("The vehicle's speed when the physics engine can use different amount of sub-steps (in m/s).")]
	public float criticalSpeed = 5f;
	[Tooltip("Simulation sub-steps when the speed is above critical.")]
	public int stepsBelow = 5;
	[Tooltip("Simulation sub-steps when the speed is below critical.")]
	public int stepsAbove = 1;

	[Tooltip("The vehicle's drive type: rear-wheels drive, front-wheels drive or all-wheels drive.")]
	public DriveType driveType;

	private WheelCollider[] m_Wheels;

	// Find all the WheelColliders down in the hierarchy.
	void Start()
	{
		playerCam = GameObject.FindGameObjectWithTag("MainCamera");
		m_Wheels = GetComponentsInChildren<WheelCollider>();


		for (int i = 0; i < m_Wheels.Length; ++i)
		{
			var wheel = m_Wheels[i];

			// Create wheel shapes only when needed.
			if (wheelShape != null)
			{
				var ws = Instantiate(wheelShape);
				ws.transform.parent = wheel.transform;
			}
		}
	}

	// This is a really simple approach to updating wheels.
	// We simulate a rear wheel drive car and assume that the car is perfectly symmetric at local zero.
	// This helps us to figure our which wheels are front ones and which are rear.
	void Update()
	{


		//yaw += speedH * Input.GetAxis("Mouse X");
		//pitch -= speedV * Input.GetAxis("Mouse Y");
		//transform.eulerAngles = new Vector3(pitch, yaw, 0.0f);
		//currentSpeed = 2 * Mathf.PI * wheelFL.radius * wheelFL.rpm * 60 / 100;
		//movingDirection = new Vector3(playerCam.transform.forward.x, 0, playerCam.transform.forward.z);
		//transform.position += movingDirection.normalized * currentSpeed * Time.deltaTime;
		



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

