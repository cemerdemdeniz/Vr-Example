using UnityEngine;
using System.Collections;

public class SteeringWheel : MonoBehaviour
{

	// Use this for initialization
	public float rotAngle = 0f;
	public float rotCoef;
	

	Transform _parent;

	void Start()
	{
		
		_parent = transform.parent.transform;
	}

	// Update is called once per frame
	void FixedUpdate()
	{

		
		
		if( rotAngle >= 0 && rotAngle < 6 )
		{
			transform.rotation = transform.parent.rotation * Quaternion.Euler(0, rotAngle, 0);

		}
		else if (rotAngle < -0.1f )
		{
			transform.rotation = transform.parent.rotation * Quaternion.Euler(0, -rotAngle, 0);
		}
		transform.rotation = _parent.rotation * Quaternion.Euler(-82f, -57f, 53f )* transform.rotation ;


	}

}