using System;
using System.Collections;
using System.Collections.Generic;
using JetBrains.Rider.Unity.Editor;
using UnityEditor.Animations;
using UnityEngine;
using UnityEngine.Networking;

[RequireComponent(typeof(Rigidbody2D))]
[RequireComponent(typeof(Collider2D))]
public class Ball : MonoBehaviour
{	
	Transform[] balls;
	Collider2D[] colliders;
	private Rigidbody2D[] rigidbodies;
	bool arrowPressed;
	
	// Use this for initialization
	void Start()
	{	
		Transform child = transform.GetChild(0);
		balls = new Transform[] { transform, child };
		colliders = new Collider2D[]{ GetComponent<Collider2D>(), child.GetComponent<Collider2D>() };
		rigidbodies = new Rigidbody2D[] { GetComponent<Rigidbody2D>(), child.GetComponent<Rigidbody2D>() };
		
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
				enableSlowMoBall();
			}
		}
		else // if !arrowPressed
		{
			if (wasPressed)
			{
				// exit slowmo
				Time.timeScale = 1;
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

	void enableSlowMoBall()
	{
		// slow mo
		Time.timeScale = 0.15f;
		
		collider.enabled = false;
		
		// swap this object with its child
		Transform child = transform.GetChild(0);
		child.SetParent(transform.parent);
		transform.SetParent(child);
	}
}
