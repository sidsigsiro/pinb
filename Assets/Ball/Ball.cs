using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Experimental.UIElements;

[RequireComponent(typeof(CircleCollider2D))]
[RequireComponent(typeof(Rigidbody2D))]
public class Ball : MonoBehaviour
{
	// constants
	
	static float NORMAL_TIME_SCALE;
	static float NORMAL_FIXED_TIME;
	static float SLOW_MO_TIME_SCALE;
	static float SLOW_MO_FIXED_TIME;
	// how many frames after releasing the arrow key does the ball fly?
	const int FRAME_TOLERANCE = 6;
	
	// store this object collider, and rigidbody
	new Rigidbody2D rigidbody;
	
	// last frame during which an arrow key was pressed
	int lastFrameHeld;

	// dictionary containing the number of the last frame an arrow key was pressed
	private Dictionary<KeyCode, ArrowKeyData> keyData;
	
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
		
		// initialize
		keyData = new Dictionary<KeyCode, ArrowKeyData>();
		keyData[KeyCode.LeftArrow] = new ArrowKeyData(KeyCode.LeftArrow, Vector2.left);
		keyData[KeyCode.RightArrow] = new ArrowKeyData(KeyCode.RightArrow, Vector2.right);
		keyData[KeyCode.UpArrow] = new ArrowKeyData(KeyCode.UpArrow, Vector2.up);
		keyData[KeyCode.DownArrow] = new ArrowKeyData(KeyCode.DownArrow, Vector2.down);

		rigidbody = GetComponent<Rigidbody2D>();
		
		lastFrameHeld = 0;
	}
	
	// Update is called once per frame
	void Update ()
	{		
		foreach (ArrowKeyData arrow in keyData.Values)
		{
			// if the key is pressed
			if (Input.GetKey(arrow.key))
			{
				arrow.framePressed = Time.frameCount;
				lastFrameHeld = Time.frameCount; // stores this frame's number
			}
		}

		print(Time.frameCount);
		
		// arrow key was pressed this frame
		if (lastFrameHeld == Time.frameCount)
		{
			// enter slowmo
			setSlowMo(true);
		}
		else if (lastFrameHeld == Time.frameCount - 1) // 1 frame ago
		{
			// exit slowmo
			setSlowMo(false);
			
			Vector2 force = Vector2.zero;

			foreach (ArrowKeyData arrow in keyData.Values)
			{
				if (arrow.framePressed >= Time.frameCount - FRAME_TOLERANCE)
				{
					print(arrow.key + ": " + arrow.framePressed);
					
					force += arrow.dir;
				}
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
