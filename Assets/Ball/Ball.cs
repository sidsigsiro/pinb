using System;
using System.Collections;
using System.Collections.Generic;
using System.Security.Cryptography;
using JetBrains.Rider.Unity.Editor;
using UnityEditor.Animations;
using UnityEngine;
using UnityEngine.Networking;

[RequireComponent(typeof(CircleCollider2D))]
[RequireComponent(typeof(Rigidbody2D))]
public class Ball : MonoBehaviour
{
	// constants
	static float NORMAL_TIME_SCALE;
	static float NORMAL_FIXED_TIME;
	static float SLOW_MO_TIME_SCALE;
	static float SLOW_MO_FIXED_TIME;
	
	// store this object collider, and rigidbody
	new Rigidbody2D rigidbody;
	
	// is arrow pressed?
	bool arrowPressed;
	
	// ---------- UNITY FUNCTIONS ----------
	
	// Use this for initialization
	void Start()
	{
		// assign the constants
		if (NORMAL_TIME_SCALE == 0)
		{
			// fraction of normal speed
			const float fraction = 3f/20f;
			
			NORMAL_TIME_SCALE = Time.timeScale;
			NORMAL_FIXED_TIME = Time.fixedDeltaTime;
			SLOW_MO_TIME_SCALE = NORMAL_TIME_SCALE * fraction;
			SLOW_MO_FIXED_TIME = NORMAL_FIXED_TIME * fraction;
		}

		rigidbody = GetComponent<Rigidbody2D>();
		
		arrowPressed = false;
	}
	
	// Update is called once per frame
	void Update ()
	{
		// save value from last frame
		bool wasPressed = arrowPressed;
		
		// true if any arrow keys is currently held
		arrowPressed = Input.GetKey(KeyCode.LeftArrow)
		                    || Input.GetKey(KeyCode.RightArrow)
		                    || Input.GetKey(KeyCode.UpArrow)
		                    || Input.GetKey(KeyCode.DownArrow);
		
		if (arrowPressed)
		{
			if (!wasPressed)
			{
				// enter slowmo
				setSlowMo(true);
				// setActiveBall(SLOW_MO);
			}
		}
		else // if !arrowPressed
		{
			if (wasPressed)
			{
				// exit slowmo
				setSlowMo(false);
				// setActiveBall(NORMAL);
			}
			
			Vector2 force = Vector2.zero;
			
			if (Input.GetKeyUp(KeyCode.LeftArrow))
			{
				force += Vector2.left;
			}
			else if (Input.GetKeyUp(KeyCode.RightArrow))
			{
				force += Vector2.right;
			}
			else if (Input.GetKeyUp(KeyCode.UpArrow))
			{
				force += Vector2.up;
			}
			else if (Input.GetKeyUp(KeyCode.DownArrow))
			{
				force += Vector2.down;
			}
			
			rigidbody.AddForce(force.normalized, ForceMode2D.Impulse);
		}
	}
	

	// ---------- HELPER FUNCTIONS ----------

	void setSlowMo(bool on)
	{
		if (on)
		{
			Time.timeScale = SLOW_MO_TIME_SCALE;
			Time.fixedDeltaTime = SLOW_MO_FIXED_TIME;
		}
		else
		{
			Time.timeScale = NORMAL_TIME_SCALE;
			Time.fixedDeltaTime = NORMAL_FIXED_TIME;
		}
	}
}
