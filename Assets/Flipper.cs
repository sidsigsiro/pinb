using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Flipper : MonoBehaviour
{
	public enum RotationDirection {clockwise, counterClockwise}
	[SerializeField]
	public RotationDirection rotationDirection = RotationDirection.counterClockwise;
	
	private float maxAngle;
	private float minAngle;
	
	private HingeJoint2D joint;
	private Rigidbody2D rigidbody;
	
	// Use this for initialization
	void Start ()
	{
		joint = GetComponent<HingeJoint2D>();
		rigidbody = GetComponent<Rigidbody2D>();

		maxAngle = joint.limits.max;
		minAngle = joint.limits.min;
	}

	void Update()
	{
	}

	void FixedUpdate () {
		JointMotor2D motor = joint.motor;
		JointAngleLimits2D limits = joint.limits;
		
		// positive motorSpeeds are clockwise
		// negative motorSpeeds are counterclockwise
		// clockwise uses a positive max limit
		// counterclockwise uses a negative max limit

		if (Input.GetKeyDown(KeyCode.Space))
		{
			Vector3 newRotation = transform.localEulerAngles;
			newRotation.z = minAngle;
			transform.localEulerAngles = newRotation;
		}
		else if (Input.GetKeyUp(KeyCode.Space))
		{
			Vector3 newRotation = transform.localEulerAngles;
			newRotation.z = maxAngle;
			transform.localEulerAngles = newRotation;
		}
		
		if (Input.GetKey(KeyCode.Space))
		{
			if (rotationDirection == RotationDirection.clockwise)
			{
				motor.motorSpeed = 10000;
			}
			else
			{
				motor.motorSpeed = -1000;
			}
		}
		else
		{
			if (rotationDirection == RotationDirection.clockwise)
			{
				motor.motorSpeed = -1000;
			}
			else
			{	
				motor.motorSpeed = 1000;
			}
		}
		
		joint.motor = motor;
	}
}
