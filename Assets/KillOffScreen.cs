using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class KillOffScreen : MonoBehaviour
{
	private SpriteRenderer _renderer;
	private int nonVisibleCount;
	
	void Start ()
	{
		_renderer = GetComponent<SpriteRenderer>();
	}
	
	void Update () {
		if (!_renderer.isVisible)
		{
			transform.position = new Vector3(0, 0, 0);
		}
	}
}
