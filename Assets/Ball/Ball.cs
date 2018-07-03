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
	// ball modes
	const int NORMAL = 0, SLOW_MO = 1;
	
	int ballMode = NORMAL;
	
	// store this object and child object's transform, collider, and rigidbody
	Transform[] balls;
	CircleCollider2D[] colliders;
	private Rigidbody2D[] rigidbodies;
	
	// is arrow pressed?
	bool arrowPressed;
	
	// ---------- UNITY FUNCTIONS ----------
	
	// Use this for initialization
	void Start()
	{	
		Transform child = transform.GetChild(0);
		balls = new []{ transform, child };
		colliders = new []{ GetComponent<CircleCollider2D>(), child.GetComponent<CircleCollider2D>() };
		rigidbodies = new []{ GetComponent<Rigidbody2D>(), child.GetComponent<Rigidbody2D>() };
		
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
				disableSlowMoBall();
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
			
			rigidbodies[ballMode].AddForce(force.normalized, ForceMode2D.Impulse);
		}
	}
	

	// ---------- HELPER FUNCTIONS ----------
	
	void enableSlowMoBall()
	{
		// slow mo
		Time.timeScale = 0.15f;
		
		colliders[NORMAL].enabled = false;
		
		swapParentwithChild();
		copyComponents(NORMAL, SLOW_MO);
		
		colliders[SLOW_MO].enabled = true;
	}

	void disableSlowMoBall()
	{
		// normal speed
		Time.timeScale = 1;
		
		colliders[SLOW_MO].enabled = false;
		
		swapParentwithChild();
		copyComponents(SLOW_MO, NORMAL);
		
		colliders[NORMAL].enabled = true;
	}
	
	// swap positions in the hierarchy
	void swapParentwithChild()
	{
		// swap this object with its child
		Transform child = transform.GetChild(0);
		child.SetParent(transform.parent);
		transform.SetParent(child);
	}

	void copyComponents(int from, int to)
	{
		copyRigidbody(rigidbodies[from], rigidbodies[to]);
		copyCircleCollider(colliders[from], colliders[to]);
	}
	
	
	void copyRigidbody(Rigidbody2D from, Rigidbody2D to)
	{
		to.angularDrag = from.angularDrag;
		to.angularVelocity = from.angularVelocity;
		to.drag = from.drag;
		to.freezeRotation = from.freezeRotation;
		to.gravityScale = from.gravityScale;
		to.inertia = from.inertia;
		to.isKinematic = from.isKinematic;
		to.mass = from.mass;
		to.position = from.position;
		to.rotation = from.rotation;
		to.velocity = from.velocity;
	}

	void copyCircleCollider(CircleCollider2D from, CircleCollider2D to)
	{
		to.radius = from.radius;
		to.density = from.density;
		to.offset = from.offset;
		to.sharedMaterial = from.sharedMaterial;
	}
}
