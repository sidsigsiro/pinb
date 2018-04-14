using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Flipper : MonoBehaviour
{
	public enum FlipType
	{
		left, right
	}
	public FlipType fliptype;
		
	float maxAngle;
	private float minAngle;
	private float direction;
	KeyCode flipbutton;
	
	private HingeJoint2D joint;
	private new Rigidbody2D rigidbody;
	
	// Use this for initialization
	private void Start ()
	{
		if (fliptype == FlipType.left)
		{
			flipbutton = KeyCode.Q;
		}
		else
		{
			flipbutton = KeyCode.E;
		}
	
		joint = GetComponent<HingeJoint2D>();
		rigidbody = GetComponent<Rigidbody2D>();

		maxAngle = joint.limits.max;
		minAngle = joint.limits.min;

		direction = Mathf.Sign(maxAngle - minAngle) * 2000;
	}

	void Update()
	{
	}

	private void FixedUpdate () {
		JointMotor2D motor = joint.motor;
		
		// positive motorSpeeds are clockwise
		// negative motorSpeeds are counterclockwise

		Vector3 newRotation = transform.localEulerAngles;
		
		if (Input.GetKeyDown(flipbutton))
		{
			// start at minAngle
			newRotation.z = minAngle;
		}
		else if (Input.GetKeyUp(flipbutton))
		{
			// start at maxAngle
			newRotation.z = maxAngle;
		}
		
		transform.localEulerAngles = newRotation;
		
		if (Input.GetKey(flipbutton))
		{
			motor.motorSpeed = direction;
			
			if (transform.rotation.z >= maxAngle)
			{
				//newRotation.z = maxAngle;
				//rigidbody.constraints &= ~RigidbodyConstraints2D.FreezeRotation;
			}
		}
		else
		{
			motor.motorSpeed = -direction;
			
			if (transform.rotation.z <= minAngle)
			{
				//newRotation.z = minAngle;
				//rigidbody.constraints |= RigidbodyConstraints2D.FreezeRotation;
			}
		}

		// assign newRotation AGAIN
		transform.localEulerAngles = newRotation;
		
		
		joint.motor = motor;
	}
}
