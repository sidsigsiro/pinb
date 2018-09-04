using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

[RequireComponent(typeof(Collider2D))]
public class Bumper : MonoBehaviour {
    void OnCollisionEnter2D(Collision2D other)
    {
        Vector2 averageNormals = other.contacts.Select(x => x.normal).Aggregate((a, b) => a + b) / other.contacts.Length;
        Rigidbody2D otherBody = other.gameObject.GetComponent<Rigidbody2D>();
        otherBody.AddForce(-averageNormals, ForceMode2D.Impulse);
    }
}
