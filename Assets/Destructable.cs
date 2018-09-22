using System.Collections;
using System.Collections.Generic;
using TypeSafety;
using UnityEngine;

[RequireComponent(typeof(Collider2D))]
public class Destructable : MonoBehaviour {
	void OnCollisionEnter2D(Collision2D other)
	{
		if (other.gameObject.layer == Layers.Ball)
		{
			Destroy(gameObject);
		}
	}
}
