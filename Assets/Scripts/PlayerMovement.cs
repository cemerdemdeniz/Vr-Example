


using UnityEngine;
using System.Collections;

public class PlayerMovement : MonoBehaviour
{

	// Use this for initialization

	public float[] rotatePoint;
	public float relYAngle;
	public float moveCoef;
	public float rotateCoef;
	public WheelMovement[] wheels;
	public SteeringWheel steeringW;
	
	public Canvas[] gameOverObject;
	public float speed, speedCoef;
	
	float currentSpeed = 5f;
	public WheelCollider wheelFR;
	public WheelCollider wheelFL;
	public float maxSpeed = 100f;
	public float maxMotorTorque = 120f;

	bool canMove;
	public Transform head;
	Rigidbody carBody;
	Vector3 relativeHeadRotation;

	public bool checkTrigger;
	float carSpeed;

	//elapsed is used to detect the time between clicks
	float elapsed = 0;

	void Start()
	{
						
		carBody = GetComponent<Rigidbody>();
		canMove = true;
		checkTrigger = false;

	}



	// Update is called once per fixed frame
	void FixedUpdate()
	{
		elapsed += Time.fixedDeltaTime;

		carSpeed = carBody.velocity.magnitude;
		relativeHeadRotation = head.transform.rotation.eulerAngles - transform.rotation.eulerAngles;
	
		// Movemnt of the car in function 
		if (canMove == true)
		{
			if (relativeHeadRotation[1] < 0)
			{
				relativeHeadRotation = relativeHeadRotation + 360 * new Vector3(0, 1, 0);
			}


			relYAngle = relativeHeadRotation[1];

			// move forward 
			if ((relYAngle > rotatePoint[1] && relYAngle < 360.0f) || (relYAngle >= 0.0f && relYAngle < rotatePoint[2]))
			{
				MoveCar(1);
			}

			else if (relYAngle > rotatePoint[0] && relYAngle < rotatePoint[1])
			{
				MoveCar(1);
				RotateCar(-Mathf.Pow(-relYAngle + rotatePoint[1], 0.5f));
			}
			else if (relYAngle > rotatePoint[2] && relYAngle < rotatePoint[3])
			{
				MoveCar(1);
				RotateCar(Mathf.Pow(relYAngle - rotatePoint[2], 0.5f));
			}

			else if (relYAngle > rotatePoint[4] && relYAngle < rotatePoint[5])
			{
				MoveCar(-1);
			}
			

		}



		MoveWheels();
		Drive();
		


	}
	private void Drive()
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


	void MoveCar(float pow)
	{
		if (checkTrigger == true)
		{
			carBody.AddForce(transform.forward * pow * moveCoef);
		}
		

	}


	void MoveWheels()
	{
		for (int ii = 0; ii <= 3; ii++)
		{
			wheels[ii].speed = carSpeed;
		}
	}


	void RotateCar(float pow)
	{
		Debug.Log("-------");
		Debug.Log(pow);



		carBody.MoveRotation(Quaternion.Euler(0, pow * rotateCoef, 0) * transform.rotation);
		// rotate Steering wheel
		steeringW.rotAngle = pow;

	}


	void OnCollisionEnter(Collision col)
	{
		string tag = col.gameObject.tag;

		if (tag == "environment" || tag == "pedestrian" || tag == "car")
		{
			canMove = false;
			gameOverObject[0].enabled = true;
			gameOverObject[1].enabled = true;

			Invoke("restart", 5);
		}
	}

	void Restart()
	{
		Application.LoadLevel(Application.loadedLevel);
	}

}
















