using UnityEngine;
using System.Collections;

public class WheelMovement : MonoBehaviour
{

	// Use this for initialization
	public float speed, speedCoef;
	float rotAngle;
	


	Transform parentT;

	void Start()
	{
		rotAngle = 0;
		speed = 0;
		speedCoef = 25;
		parentT = transform.parent.transform;
	}

	// Update is called once per frame
	void FixedUpdate()
	{
		rotAngle += speed * Time.fixedDeltaTime;

		transform.rotation = parentT.rotation * Quaternion.Euler(0, 0, 0);

	}
	

}
