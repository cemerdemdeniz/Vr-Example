using UnityEngine;
using System.Collections;

public class WheelMovement : MonoBehaviour
{

	// Use this for initialization
	public float speed, speedCoef;

	Transform parentT;
	void Start()
	{
		
		parentT = transform.parent.transform;
	}

	// Update is called once per frame
	void FixedUpdate()
	{
		transform.rotation = parentT.rotation * Quaternion.Euler(0, 0, 0);

	}
	

}
