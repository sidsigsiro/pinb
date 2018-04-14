using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class KillBox : MonoBehaviour {
    void OnTriggerEnter2D(Collider2D c)
    {
        // reload current scene
        if (c.name.ToLower() == "ball")
        {
            GameObject.Find("Ball").transform.position = new Vector3(-2, 0, 0);
        }
    }
}
