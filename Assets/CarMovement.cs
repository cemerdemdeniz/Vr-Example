using UnityEngine;
using System.Collections;

public class CarMovement : MonoBehaviour
{

	// Use this for initialization

	public float[] alpha;
	public float relYAngle;
	public float moveCoef;
	public float rotateCoef;
	public WheelMovement[] wheels;
	public SteeringWheel steeringW;
	//public SpeedMeter speedMet;
	public Canvas[] gameOverObject;

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


		// carboard head contains the rotation of the player (where the player is looking at)

		carBody = GetComponent<Rigidbody>();
		canMove = true;
		checkTrigger = false;

	}



	// Update is called once per fixed frame
	void FixedUpdate()
	{
		elapsed += Time.fixedDeltaTime;

		// obtain speed of the car
		carSpeed = carBody.velocity.magnitude;


		// the angle between the car and the head is obtained
		relativeHeadRotation = head.transform.rotation.eulerAngles - transform.rotation.eulerAngles;

		//standard value [0-360]




		if (Input.GetMouseButton(0) == true || Input.GetButton("Fire1") == true && elapsed > 0.5f)
		{
			elapsed = 0;


			if (checkTrigger == true)
			{
				checkTrigger = false;
			}
			else
			{
				checkTrigger = true;
			}
		}

		//float v=Input.GetAxis("Vertical");

		// Movemnt of the car in function 
		if (canMove == true)
		{
			if (relativeHeadRotation[1] < 0)
			{
				relativeHeadRotation = relativeHeadRotation + 360 * new Vector3(0, 1, 0);
			}


			relYAngle = relativeHeadRotation[1];

			// move forward 
			if ((relYAngle > alpha[1] && relYAngle < 360.0f) || (relYAngle >= 0.0f && relYAngle < alpha[2]))
			{
				moveCar(1);
			}

			else if (relYAngle > alpha[0] && relYAngle < alpha[1])
			{
				moveCar(1);
				rotateCar(-Mathf.Pow(-relYAngle + alpha[1], 0.5f));
			}
			else if (relYAngle > alpha[2] && relYAngle < alpha[3])
			{
				moveCar(1);
				rotateCar(Mathf.Pow(relYAngle - alpha[2], 0.5f));
			}

			else if (relYAngle > alpha[4] && relYAngle < alpha[5])
			{
				moveCar(-1);
			}
			/*else
			{
				moveCar(0);
			}*/

		}



		moveWheels();
		


	}


	void moveCar(float pow)
	{
		if (checkTrigger == true)
		{
			carBody.AddForce(transform.forward * pow * moveCoef);
		}
		//carBody.MovePosition(transform.position+transform.forward*pow*moveCoef);

	}


	void moveWheels()
	{
		for (int ii = 0; ii <= 3; ii++)
		{
			wheels[ii].speed = carSpeed;
		}
	}


	void rotateCar(float pow)
	{

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

	void restart()
	{
		Application.LoadLevel(Application.loadedLevel);
	}

}
