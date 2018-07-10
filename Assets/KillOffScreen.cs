using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class KillOffScreen : MonoBehaviour
{
	new SpriteRenderer renderer;
	new Rigidbody2D rigidbody;

	Vector3 startPos;
	
	int nonVisibleCount;
	
	void Start ()
	{
		startPos = transform.position;
		
		renderer = GetComponent<SpriteRenderer>();
		rigidbody = GetComponent<Rigidbody2D>();
	}
	
	void Update () {
		if (!renderer.isVisible)
		{
			transform.position = startPos;
			
			if (rigidbody != null)
			{
				rigidbody.velocity = Vector2.zero;
				rigidbody.angularVelocity = 0;
			}
		}
	}
}
