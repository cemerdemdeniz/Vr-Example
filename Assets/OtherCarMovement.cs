using UnityEngine;
using System.Collections;

public class OtherCarMovement : MonoBehaviour {


	// Use this for initialization
	Rigidbody carBody;
	public float speed=10;
	public WheelMovement[] wheels;
		
	void Start () 
	{
		carBody=GetComponent<Rigidbody>();
				
	}
	
	// Update is called once per frame
	void FixedUpdate () 
	{
		
		carBody.velocity=speed*transform.forward;



		for(int ii=0;ii<=3;ii++)
		{
			wheels[ii].speed=speed;
		}

	}
	
	void OnTriggerEnter(Collider col)
	{
		
		string tag=col.gameObject.tag;
		
		// hit the wall and change crossing direction
		if(tag=="carLimits")
		{

			Destroy(gameObject);
		}

				

	}
	


}
