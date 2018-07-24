using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

[RequireComponent(typeof(Collider2D))]
public class Bumper : MonoBehaviour {

	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		
	}

	void OnCollisionEnter(Collision other)
	{
		Vector3 averageNormals = other.contacts.Select(x => x.normal).Aggregate((a, b) => a + b) / other.contacts.Length;

		Rigidbody2D otherBody = other.gameObject.GetComponent<Rigidbody2D>();

		otherBody.AddForce(averageNormals, ForceMode2D.Impulse);
		
		print("hi");
	}
}
